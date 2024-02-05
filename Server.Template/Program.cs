using Microsoft.EntityFrameworkCore;
using $safeprojectname$.Data;
using $safeprojectname$.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MainDbContext>(option =>
                                        option.UseSqlServer("Data Source=129.232.214.205,8520;Initial Catalog=Running;Persist Security Info=True;User ID=CapriComUser;Password=CapriComUser;Encrypt=True;TrustServerCertificate=True;"));

//DI

builder.Services.AddScoped<AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#if !DEBUG
app.UseHttpsRedirection();
#endif

app.UseAuthorization();

app.MapControllers();

app.Run();
