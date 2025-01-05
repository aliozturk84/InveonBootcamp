using InveonBootcamp.Business.Abstract;
using InveonBootcamp.Business.Concrete;
using InveonBootcamp.Business.TokenOperations.Models;
using InveonBootcamp.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
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
        private readonly UserManager<User> _userManager;


        public TokenHandler(IConfiguration config, UserManager<User> userManager)
        {
            _config = config;
            _userManager = userManager;
        }
        public async Task<Token> CreateAccessToken(User user)
        {
            var role = await _userManager.GetRolesAsync(user);
            var isInstructor = role.Any(x=>x.Contains("Eğitmen"))? true : false;

            Token token = new Token();
            var claims = new Claim[]
            {
               new Claim("userEmail",$"{user.Email}"),
               new Claim("userId",$"{user.Id}"),
               new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
               isInstructor ? new Claim(ClaimTypes.Role, "Eğitmen") : null

            }.Where(c=>c!=null).ToArray();

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
