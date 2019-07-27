using LuizaLabs.Domain.Entities;
using LuizaLabs.Infra.Cross;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LuizaLabs.Domain.Service
{
    public class SrvAuthentication : ISrvAuthentication
    {
        private readonly IConfiguration _configuration;

        public SrvAuthentication(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public object Authentication(Auth auth)
        {
            auth.Validate(_configuration.GetSection("AppSettings:Usuario").Value, _configuration.GetSection("AppSettings:Senha").Value);
            return GerarJWT(_configuration.GetSection("AppSettings:SecretKeyAuth").Value, auth);

        }

        private object GerarJWT(string chaveSecreta, Auth auth)
        {
            if (string.IsNullOrWhiteSpace(chaveSecreta))
                throw new ValidationException("Chave não informada.");

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, auth.Login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.Add(new Claim(ClaimTypes.Role, "API"));
               

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new 
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };


        }
    }
}
