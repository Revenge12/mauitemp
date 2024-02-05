using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using $safeprojectname$.Data;
using Shared.Template;
using Shared.Template.AuthModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace $safeprojectname$.Services
{
    public class AuthService
    {
        private readonly MainDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(MainDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public static Userstbl user = new Userstbl();

        public async Task<ServiceResponse<int>> Register(Registration request)
        {
            try
            {

                if (!await CheckIfUserExisits(request))
                {
                    user.Usernamefld = request.Username;
                    user.PasswordHashfld = CreatePasswordHash(request.Password);
                    user.EmailAddressfld = request.EmailAddress;

                    await _context.AddRangeAsync(user);
                    await _context.SaveChangesAsync();

                    return new ServiceResponse<int>()
                    {
                        Message = "User created",
                        Success = true,
                    };
                }
                else
                {
                    throw new Exception("User already exisits");
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse<int>()
                {
                    Message = ex.Message,
                    Success = false
                };
            }


        }

        public async Task<ServiceResponse<string>> Login(UserDto request)
        {
            try
            {
                var user = await ReturnUser(request);

                var isVerified = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHashfld);

                if (isVerified)
                {
                    var token = CreateToken(user, request.RememberMe);
                    return new ServiceResponse<string>
                    {
                        Success = true,
                        Message = "User logged in",
                        Data = token
                    };

                }

                throw new Exception("Username/Password is incorrect");
            }
            catch (Exception ex)
            {
                return new ServiceResponse<string>()
                {
                    Message = ex.Message,
                    Success = false
                };
            }
        }

        private string CreateToken(Userstbl user, bool stayLoggedIn)
        {
            if(user is null)
            {
                throw new Exception("User was not found");
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserGuidfld.ToString()),
                new Claim(ClaimTypes.Email, user.EmailAddressfld.ToString()),
                new Claim(ClaimTypes.Name, user.Usernamefld.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            if (stayLoggedIn == true)
            {
                var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddYears(1), signingCredentials: creds);
                var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                return jwt;

            }
            else
            {
                var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);
                var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                return jwt;
            }


        }

        private async Task<Userstbl> ReturnUser(UserDto request)
        {
            var findUser = await _context.Userstbl.Where(x => x.Usernamefld == request.Username).FirstOrDefaultAsync();

            if (findUser is null)
            {
                findUser = await _context.Userstbl.Where(x => x.EmailAddressfld == request.Username).FirstOrDefaultAsync();

                if (findUser is null)
                {
                    throw new Exception("Username/Password is incorrect");
                }
            }

            return findUser;
        }

        private string CreatePasswordHash(string password)
        {
            if(password is null)
            {
                throw new Exception("Password cannot be null");
            }
            else
            {
                return BCrypt.Net.BCrypt.HashPassword(password);
            }

        }

        private async Task<bool> CheckIfUserExisits(Registration request)
        {
            try
            {
                var user = await _context.Userstbl.Where(x => x.Usernamefld == request.Username).FirstOrDefaultAsync();

                if (user == null)
                {
                    _context.ChangeTracker.Clear();
                    user = await _context.Userstbl.Where(x => x.EmailAddressfld == request.EmailAddress).FirstOrDefaultAsync();

                    if (user == null)
                    {
                        return false;
                    }

                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return true;
            }

        }

    }
}
