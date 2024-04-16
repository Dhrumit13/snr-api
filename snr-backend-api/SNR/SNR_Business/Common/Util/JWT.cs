using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SNR_Business.Common.Util
{
    public class Jwt
    {
        public static string GetJwtToken(TokenInfo utEntity)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, utEntity.userId),
                new Claim(ClaimTypes.Name, utEntity.userName),
                //new Claim("userId", utEntity.userId),
                //new Claim("userName", utEntity.userName),
                //new Claim("firstName", utEntity.firstName),
                //new Claim("lastName", utEntity.lastName),
                //new Claim("groupId", utEntity.groupId),
                //new Claim("locationId", utEntity.locationId),
                //new Claim("isSuperAdmin", utEntity.isSuperAdmin),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now.ToUniversalTime()).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.ToUniversalTime().AddMinutes(utEntity.tokenExpireInMin)).ToUnixTimeSeconds().ToString()),
            };
            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EncryptDecrypt.Base64Encode())),
                                             SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class TokenInfo
    {
        public string userId { get; set; }
        public string userName { get; set; }
        public long tokenExpireInMin { get; set; }
    }
}
