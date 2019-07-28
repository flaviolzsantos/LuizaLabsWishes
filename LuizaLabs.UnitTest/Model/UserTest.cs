using LuizaLabs.Domain.Entities;
using LuizaLabs.Infra.Cross;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace LuizaLabs.UnitTest.Model
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void UsuarioSemListaDesejo()
        {
            User user = new User();
            user.ValidateWishes();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UsuarioSemNome()
        {
            User user = new User() { Email = "email@teste" };
            user.ValidateUser();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UsuarioSemEmail()
        {
            User user = new User() { Name = "nome" };
            user.ValidateUser();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UsuarioEmailIvalido()
        {
            User user = new User() { Name = "nome", Email = "email" };
            user.ValidateUser();
        }

        [TestMethod]
        public void UsuarioValido()
        {
            User user = new User() { Name = "nome", Email = "email@email.com" };
            user.ValidateUser();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UsuarioListaDesejoPaginaInvalida()
        {
            User user = new User();
            user.GetWishesPagination(10, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UsuarioListaDesejoTamanhoPaginaInvalida()
        {
            User user = new User();
            user.GetWishesPagination(-1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void UsuarioListaDesejoNulo()
        {
            User user = new User();
            user.GetWishesPagination(10, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void UsuarioListaDesejoVazia()
        {
            User user = new User();
            user.AddWishes(new List<Product>());
            user.GetWishesPagination(10, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void UsuarioListaDesejoPaginacaoInvalida()
        {
            User user = new User();
            user.AddWishes(new List<Product>() { new Product(), new Product() });
            user.GetWishesPagination(10, 2);
        }

        [TestMethod]
        public void UsuarioListaDesejoPaginacaoValida()
        {
            User user = new User();
            user.AddWishes(new List<Product>() { new Product(), new Product() });
            var list = user.GetWishesPagination(1, 2);
            Assert.IsNotNull(list);
            Assert.AreEqual(list.Count(), 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UsuarioAddListaDesejoInvalida()
        {
            User user = new User();
            user.AddWishes(null);
        }

        [TestMethod]
        public void UsuarioAddListaDesejoValida()
        {
            User user = new User();
            user.AddWishes(new List<Product>());
        }
    }
}
