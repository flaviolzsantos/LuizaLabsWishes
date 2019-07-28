using LuizaLabs.Domain.Entities;
using LuizaLabs.Infra.Cross;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuizaLabs.UnitTest.Model
{
    [TestClass]
    public class AuthTest
    {
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LoginNulo()
        {
            Auth auth = new Auth();
            auth.Validate(null, "123");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SenhaNula()
        {
            Auth auth = new Auth();
            auth.Validate("login", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LoginIncorreto()
        {
            Auth auth = new Auth() { Login = "login2", Senha = "123" };
            auth.Validate("login", "123");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SenhaIncorreto()
        {
            Auth auth = new Auth() { Login = "login", Senha = "1234" };
            auth.Validate("login", "123");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void LoginSenhaIncorreto()
        {
            Auth auth = new Auth() { Login = "login2", Senha = "1234" };
            auth.Validate("login", "123");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GerarTokenSemChave()
        {
            Auth auth = new Auth();
            auth.GerarJWT(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GerarTokenComChaveSemLogin()
        {
            Auth auth = new Auth();
            auth.GerarJWT("123");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GerarTokenComChaveMenorIgual15Caracter()
        {
            Auth auth = new Auth() { Login = "teste" };
            auth.GerarJWT("123456781234567");
        }

        [TestMethod]
        public void GerarTokenComChaveValida()
        {
            Auth auth = new Auth() { Login = "teste" };
            object token = auth.GerarJWT("1234567812345678");
            Assert.IsNotNull(token);
        }
    }
}
