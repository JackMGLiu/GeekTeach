using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Geek.Framework.Jwt
{
    public static class JwtExtensions
    {
        public static AuthenticationBuilder AddJwtBearer(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                           .AddJwtBearer(options =>
                           {
                               options.TokenValidationParameters = new TokenValidationParameters()
                               {
                                   ValidIssuer = configuration["JwtOptions:Issuer"],
                                   ValidAudience = configuration["JwtOptions:Audience"],
                                   ValidateIssuer = true,
                                   ValidateLifetime = true,
                                   ValidateIssuerSigningKey = true,
                                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtOptions:SecurityKey"])),
                                   ClockSkew = TimeSpan.Zero

                               };
                               options.Events = new JwtBearerEvents
                               {
                                   OnMessageReceived = context =>
                                   {
                                       var authorization = context.Request.Headers["Token"];
                                       var token = authorization.FirstOrDefault();
                                       if (token != null)
                                       {
                                           context.Request.Headers.Remove("Authorization");
                                           context.Request.Headers.Add("Authorization", $"Bearer {token}");
                                       }
                                       return Task.CompletedTask;
                                   },
                                   OnAuthenticationFailed = context =>
                                   {
                                       if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                                       {
                                           context.Response.Headers.Add("Token-Expired", "true");
                                       }
                                       return Task.CompletedTask;
                                   }
                               };
                           });

        }
    }
}
