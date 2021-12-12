using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApplication1
{
    public  class JwtAuthenticationService: IJwtAuthenticationService
    {
        private readonly string _key;
        private readonly string _Expytoken;

        public JwtAuthenticationService(string key, string ExpyToken)
        {
            _key = key;
            _Expytoken = ExpyToken;
        }

        public string Authenticate(string mailUnlam, string password) {

            if (string.IsNullOrEmpty(mailUnlam) || string.IsNullOrEmpty(password)) {

                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity( new Claim[] 
                {
                    new Claim(ClaimTypes.Email, mailUnlam)
                }), 
                Expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_Expytoken)),
                SigningCredentials = new SigningCredentials( new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}