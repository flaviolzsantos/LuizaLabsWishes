using LuizaLabs.Domain.Entities;
using LuizaLabs.Domain.Service;
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
    public class SrvWishTest
    {
        private readonly Mock<ISrvUser> _srvUser;
        private readonly Mock<ISrvProduct> _srvProduct;
        private readonly ISrvWish _srvWish;

        public SrvWishTest()
        {
            _srvUser = new Mock<ISrvUser>();
            _srvProduct = new Mock<ISrvProduct>();
            _srvWish = new SrvWish(_srvUser.Object, _srvProduct.Object);
        }

        [TestMethod]
        public void CriarListaDesejo()
        {
            _srvUser.Setup(x => x.GetUserAsync(It.IsAny<string>())).ReturnsAsync(new User());
            _srvProduct.Setup(x => x.GetProductAsync(It.IsAny<List<Wish>>())).ReturnsAsync(new List<Product>());
            _srvUser.Setup(x => x.UpdateUserAsync(It.IsAny<User>()));
            _srvWish.CreateWishesAsync(It.IsAny<List<Wish>>(), It.IsAny<string>());

        }

        [TestMethod]
        public async Task ObterPaginacaoAsync()
        {
            User user = new User();
            user.AddWishes(new List<Product>() { new Product() });
            _srvUser.Setup(x => x.GetUserAsync(It.IsAny<string>())).ReturnsAsync(user);
            IEnumerable<Product> listProduct = await _srvWish.GetPaginationAsync(1, 1, It.IsAny<string>());
            Assert.IsNotNull(listProduct);
        }
    }
}
