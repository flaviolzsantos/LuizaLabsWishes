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
    public class SrvProductTest
    {
        private readonly Mock<IRepProduct> _repProductMoq;
        private readonly ISrvProduct _srvproduct;
        private readonly Product _product;
        public SrvProductTest()
        {
            _repProductMoq = new Mock<IRepProduct>();
            _srvproduct = new SrvProduct(_repProductMoq.Object);
            _product = new Product() { Id = Guid.NewGuid(), Name = "Teste" };
        }

        [TestMethod]
        [ExpectedException(typeof(NotContentException))]
        public async Task GravarProdutoNuloAsync()
        {
            await _srvproduct.AddAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(AlreadyExistException))]
        public async Task GravarProdutoExistenteAsync()
        {
            _repProductMoq.Setup(x => x.HasName(It.IsAny<string>())).ReturnsAsync(true);
            await _srvproduct.AddAsync(_product);
        }

        [TestMethod]
        public async Task GravarProdutoValidoAsync()
        {
            _repProductMoq.Setup(x => x.HasName(It.IsAny<string>())).ReturnsAsync(false);
            _repProductMoq.Setup(x => x.Add(It.IsAny<Product>()));
            await _srvproduct.AddAsync(_product);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task ObterProdutoPaginacaoPaginaInvalidaAsync()
        {
            await _srvproduct.GetPaginationAsync(10, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task ObterProdutoPaginacaoTamanhoPaginaInvalidaAsync()
        {
            await _srvproduct.GetPaginationAsync(-1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task ObterProdutoPaginacaoSemListaDesejoAsync()
        {
            _repProductMoq.Setup(x => x.GetPaginationAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string[]>())).ReturnsAsync(new List<Product>());
            await _srvproduct.GetPaginationAsync(1, 1);
        }

        [TestMethod]
        public async Task ObterUsuarioPaginacaoListaDesejoValidaAsync()
        {
            _repProductMoq.Setup(x => x.GetPaginationAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string[]>())).ReturnsAsync(new List<Product>() { new Product() });
            List<Product> listProduct = await _srvproduct.GetPaginationAsync(1, 1);
            Assert.IsNotNull(listProduct);
            Assert.AreEqual(listProduct.Count, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(NotContentException))]
        public async Task ObterProdutoListaDesejoNulo()
        {
            await _srvproduct.GetProductAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotContentException))]
        public async Task ObterProdutoListaDesejoVazia()
        {
            await _srvproduct.GetProductAsync(new List<Wish>());
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task ObterProdutoListaDesejoGuidIncorretoErrado()
        {
            await _srvproduct.GetProductAsync(new List<Wish>() { new Wish() { IdProduct = "guidErrado" } });
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task ObterProdutoListaDesejoGuidErrado()
        {
            Product product = null;
            _repProductMoq.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(product);
            await _srvproduct.GetProductAsync(new List<Wish>() { new Wish() { IdProduct = Guid.NewGuid().ToString() } });
        }

        [TestMethod]
        public async Task ObterProdutoListaDesejo()
        {
            _repProductMoq.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Product());
            List<Product> listProduct = await _srvproduct.GetProductAsync(new List<Wish>() { new Wish() { IdProduct = Guid.NewGuid().ToString() } });
            Assert.IsNotNull(listProduct);
            Assert.AreEqual(listProduct.Count, 1);
        }
    }
}
