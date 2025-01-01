using InveonBootcamp.Business.TokenOperations.Models;
using InveonBootcamp.Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.Business.TokenOperations
{
    public class TokenHandler
    {
        public readonly IConfiguration _config;

        public TokenHandler(IConfiguration config)
        {
            _config = config;
        }
        public Token CreateAccessToken(User user)
        {
            Token token = new Token();
            var claims = new Claim[]
            {
               new Claim("userEmail",$"{user.Email}")
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:SecurityKey"]));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            token.ExpirationDate = DateTime.Now.AddMinutes(15);
            JwtSecurityToken securityToken = new(
                issuer: _config["Token:Issuer"],
                audience: _config["Token:Audience"],
                claims: claims,
                expires: token.ExpirationDate,
                notBefore: DateTime.Now,
                signingCredentials: credentials);

            JwtSecurityTokenHandler tokenHandler = new();

            //Token yaratılıyor.
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            token.RefreshToken = CreateRefreshToken();

            return token;
        }
        public string CreateRefreshToken() { return Guid.NewGuid().ToString(); }
    }
}
