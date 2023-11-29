using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QPL.BL.Token
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _confinguration;
        public TokenHandler(IConfiguration configuration)
        {
            _confinguration = configuration;
        }
        public DTOs.Token CreateAccessToken(int minute)
        {
            DTOs.Token token = new();

            //Security Key'in simetriğini alıyoruz.
            //SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_confinguration["Token:SecurityKey"]));

            //Şifrelenmiş kimliği oluşturuyoruz.
            //SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            //Oluşturulacak token ayarlarını veriyoruz.
            //token.Expiration = DateTime.UtcNow.AddMinutes(minute);
            //JwtSecurityToken securityToken = new(
            //    audience : _confinguration["Token:Audience"],
            //    issuer : _confinguration["Token:Issuer"],
            //    expires : token.Expiration,
            //    notBefore: DateTime.UtcNow, //Token üretildiği andan ne zaman sonra devreye girsin
            //    signingCredentials : signingCredentials
            //    );

            ////Token oluşturucu sınıfından örnek alıyoruz
            //JwtSecurityTokenHandler tokenHandler = new();
            //token.AccessToken = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
