using System;
namespace Geek.Framework.Jwt
{
    public class JwtSessionModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
