using LuizaLabs.Infra.Cross;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LuizaLabs.Domain.Entities
{
    public class Auth
    {
        public string Login { get; set; }
        public string Senha { get; set; }

        public void Validate(string login, string senha)
        {
            if (string.IsNullOrEmpty(Login))
                throw new ValidationException("Login é requerido");

            if (string.IsNullOrEmpty(Senha))
                throw new ValidationException("Senha é requerida");

            if (!Login.Equals(login) || !Senha.Equals(senha))
                throw new ValidationException("Usuário ou senha incorreto");
        }

        public object GerarJWT(string chaveSecreta)
        {
            if (string.IsNullOrWhiteSpace(chaveSecreta))
                throw new ValidationException("Chave não informada.");

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, Login),
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
