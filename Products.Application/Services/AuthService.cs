using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Products.Application.Services
{
    public class AuthService
    {
        private readonly IConfiguration _config;
        public AuthService(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateJWTToken(string username, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:key"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                 issuer: _config["JwtSettings:Issuer"],
                 audience: _config["JwtSettings:audience"],
                 claims: claims,
                 expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["JwtSettings:ExpiryMinutes"])),
                 signingCredentials: credential
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
