using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Geek.Framework.Extensions;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Geek.Framework.Jwt
{
    /// <summary>
    /// Token帮助类
    /// </summary>
    public class TokenHelper
    {
        private static string _secret = ConfigExtensions.Configuration["JwtOptions:SecurityKey"];
        private static string _issuer = ConfigExtensions.Configuration["JwtOptions:Issuer"];
        private static string _audience = ConfigExtensions.Configuration["JwtOptions:Audience"];

        public static string GenerateToken(JwtSessionModel sessionModel, int expireHour = 24)
        {
            var key1 = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var cred = new SigningCredentials(key1, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("sid",sessionModel.UserId),
                new Claim("username",sessionModel.UserName),
                new Claim("rid",sessionModel.RoleId),
                new Claim("rolename",sessionModel.RoleName)
                //new Claim(ClaimTypes.Name,name), //示例  可使用ClaimTypes中的类型
            };
            var token = new JwtSecurityToken(
                issuer: _issuer,//签发者
                audience: _audience,
                notBefore: DateTime.Now,//token不能早于这个时间使用
                expires: DateTime.Now.AddHours(expireHour),//添加过期时间
                claims: claims,//签名数据
                signingCredentials: cred//签名
                );
            //解决一个不知什么问题的PII什么异常
            IdentityModelEventSource.ShowPII = true;
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
