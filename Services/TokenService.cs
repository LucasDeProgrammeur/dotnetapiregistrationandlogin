using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using auth_api.Models;
using Microsoft.IdentityModel.Tokens;

namespace auth_api.Services
{
    public class TokenService : Interfaces.ITokenService
    {

        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        // haal data van appsettings.json
        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]!));
        }
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName!)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Claimsidentity is de verzameling van de claims
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //return the token object as a string
            return tokenHandler.WriteToken(token);
        }
    }
}