using Shared.Template;
using Shared.Template.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace $safeprojectname$.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ServiceResponse<string>> Login(UserDto user)
        {
            var result = await _http.PostAsJsonAsync("/api/Auth/login", user);

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
            }
            else
            {
                return new ServiceResponse<string> { Message = "Could not login", Success = false };
            }
        }


       
        public async Task<bool> GetAuthentication()
        {
            await Task.Delay(2000);

            var auth = await SecureStorage.GetAsync("auth");

            if(auth is null)
                return false;

            return true;
        }

        public List<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var roles = new List<string>();

            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            claims.Add(keyValuePairs.Select(kvp => new Claim(ClaimTypes.NameIdentifier, kvp.Value.ToString())).FirstOrDefault());

            var findUser = keyValuePairs.Where(x => x.Key == ClaimTypes.Name).FirstOrDefault();

            var expValue = keyValuePairs.Where(x => x.Key == "exp").FirstOrDefault();

            var test = expValue.Value.ToString();

            var expirationTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(test)).DateTime;


            if (expirationTime <= DateTime.Now)
            {
                claims = new List<Claim>();
                return claims;
            }

            claims.Add(new Claim(ClaimTypes.Role, findUser.Value.ToString()));

            //var role = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));

            try
            {
                var findRole = keyValuePairs.Where(x => x.Key == ClaimTypes.Role).FirstOrDefault();

                if (roles.Count > 0)
                {
                    roles = findRole.Value.ToString().Split(',').ToList();

                    foreach (var role in roles)
                    {
                        var replacementValue = role.Replace("[", "").Replace("]", "").Replace("\"", "").ToUpper();
                        claims.Add(new Claim(ClaimTypes.Role, replacementValue));
                    }
                }



            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }



            return claims;

        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
