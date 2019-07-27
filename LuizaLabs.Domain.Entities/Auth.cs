using LuizaLabs.Infra.Cross;
using System;
using System.Collections.Generic;
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
    }
}
