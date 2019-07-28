using LuizaLabs.Domain.Entities;
using LuizaLabs.Infra.Cross;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuizaLabs.UnitTest.Model
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ProdutoSemNome()
        {
            Product product = new Product();
            product.ValidateProduct();

        }

        [TestMethod]
        public void ProdutoValido()
        {
            Product product = new Product() { Name = "teste" };
            product.ValidateProduct();

        }
    }
}
