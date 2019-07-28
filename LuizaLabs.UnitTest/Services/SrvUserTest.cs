using LuizaLabs.Domain.Entities;
using LuizaLabs.Domain.Service;
using LuizaLabs.Infra.Cross;
using LuizaLabs.Infra.Data.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuizaLabs.UnitTest.Services
{
    [TestClass]
    public class SrvUserTest
    {
        private readonly Mock<IRepUser> _repUserMoq;
        private readonly ISrvUser _srvUser;
        private readonly User _user;
        public SrvUserTest()
        {
            _repUserMoq = new Mock<IRepUser>();
            _srvUser = new SrvUser(_repUserMoq.Object);
            _user = new User() { Email = "email@email.com", Name = "nome", Id = Guid.NewGuid() };
        }

        [TestMethod]
        [ExpectedException(typeof(NotContentException))]
        public async Task GravarUsuarioNuloAsync()
        {
            await _srvUser.AddAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(AlreadyExistException))]
        public async Task GravarUsuarioExistenteAsync()
        {
            _repUserMoq.Setup(x => x.HasName(It.IsAny<string>())).ReturnsAsync(true);
            await _srvUser.AddAsync(_user);
        }

        [TestMethod]
        public async Task GravarUsuarioValidoAsync()
        {
            _repUserMoq.Setup(x => x.HasName(It.IsAny<string>())).ReturnsAsync(false);
            _repUserMoq.Setup(x => x.Add(It.IsAny<User>()));
            await _srvUser.AddAsync(_user);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task ObterUsuarioPaginacaoPaginaInvalidaAsync()
        {
            await _srvUser.GetPaginationAsync(10, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task ObterUsuarioPaginacaoTamanhoPaginaInvalidaAsync()
        {
            await _srvUser.GetPaginationAsync(-1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task ObterUsuarioPaginacaoSemListaDesejoAsync()
        {
            _repUserMoq.Setup(x => x.GetPaginationAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string[]>())).ReturnsAsync(new List<User>());
            await _srvUser.GetPaginationAsync(1, 1);
        }

        [TestMethod]
        public async Task ObterUsuarioPaginacaoListaDesejoValidaAsync()
        {
            _repUserMoq.Setup(x => x.GetPaginationAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string[]>())).ReturnsAsync(new List<User>() {new User() });
            List<User> listUser = await _srvUser.GetPaginationAsync(1, 1);
            Assert.IsNotNull(listUser);
            Assert.AreEqual(listUser.Count, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(NotContentException))]
        public async Task ObterUsuarioParametroNulo()
        {            
            await _srvUser.GetUserAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task ObterUsuarioParametroGuidErrado()
        {
            await _srvUser.GetUserAsync("guidErrado");
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task ObterUsuarioInexistente()
        {
            User user = null;
            _repUserMoq.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(user);
            await _srvUser.GetUserAsync(Guid.NewGuid().ToString());
        }

        [TestMethod]
        public async Task ObterUsuarioExistente()
        {
            User user = new User() { Email = "teste@teste.com", Name = "teste" };
            _repUserMoq.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(user);
            User usertReturn = await _srvUser.GetUserAsync(Guid.NewGuid().ToString());
            Assert.IsNotNull(user);
            Assert.AreEqual(user.Name, usertReturn.Name);
            Assert.AreEqual(user.Email, usertReturn.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task AlterarUsuarioNulo()
        {
            User user = null;
            await _srvUser.UpdateUserAsync(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task AlterarUsuarioGuidErrado()
        {
            User user = new User();
            await _srvUser.UpdateUserAsync(user);
        }

        [TestMethod]
        public async Task AlterarUsuarioCorreto()
        {
            _repUserMoq.Setup(x => x.Update(It.IsAny<User>(), It.IsAny<Guid>()));
            await _srvUser.UpdateUserAsync(_user);
        }
    }
}
