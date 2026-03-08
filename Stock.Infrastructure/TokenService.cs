using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StockApp.Core.Interfaces;
using StockApp.StockApp.Core.Interfaces;
using StockApp.StockApp.Core.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockApp.Infrastructure
{
    public class TokenService:ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateToken(StockUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var expiresAt = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMins"]));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDiscriptal = new JwtSecurityToken(issuer, audience, claims, expires: expiresAt, signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDiscriptal);
        }
    }
}
