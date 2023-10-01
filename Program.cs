using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TokenDotNetCore;
using TokenDotNetCore.Models;
using TokenDotNetCore.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(cfg => {
    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters { 

        IssuerSigningKey = new  SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.Secret)), 
        ValidateIssuer = false, 
        ValidateAudience = false
    }; 

}); 

builder.Services.AddAuthorization();

builder.Services.AddTransient<TokenSerive>();
builder.Services.AddScoped<CreateTokenService>(); 

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization(); 

app.MapGet("/", ([FromServices] TokenSerive serive) => {
    var user = new User(1, "raul01mateia@gmail.com", "123654", new[] {"Admin", "Tecnico"});
    return serive.Create(user); 
    });

app.MapGet("/autorizado", () =>("Autorizado por aceesso ")).RequireAuthorization();
app.MapGet("/admin", () => ("Autorizado por aceesso ")).RequireAuthorization("admin");

app.MapGet("/token", ([FromServices] CreateTokenService tokenService) => tokenService.CreateToken());

app.Run();
