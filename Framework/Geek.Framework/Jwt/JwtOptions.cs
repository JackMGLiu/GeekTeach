using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Geek.Framework.Jwt
{
    public class JwtOptions
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string SecurityKey { get; set; }

        public SymmetricSecurityKey SymmetricSecurityKey => string.IsNullOrEmpty(SecurityKey) ?
                                                            throw new ArgumentNullException(nameof(SecurityKey)) :
                                                            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecurityKey));

        public SigningCredentials SigningCredentials => new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);
    }
}
