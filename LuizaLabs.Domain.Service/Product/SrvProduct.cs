using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LuizaLabs.Domain.Entities;
using LuizaLabs.Infra.Cross;
using LuizaLabs.Infra.Data.Interfaces;
using LuizaLabs.Infra.Data.Repository;
using System.Linq;

namespace LuizaLabs.Domain.Service
{
    public class SrvProduct : ISrvProduct
    {
        private readonly IRepProduct _repProduct;
        public SrvProduct(IRepProduct repProduct)
        {
            _repProduct = repProduct;
        }

        public async Task AddAsync(Product product)
        {
            if (product == null)
                throw new NotContentException(string.Empty);

            product.ValidateProduct();

            if (await _repProduct.HasName(product.Name))
                throw new AlreadyExistException("Não foi possível cadastrar esse produto pois ele já está cadastrado");

            await _repProduct.Add(product);
        }

        public async Task<List<Product>> GetPaginationAsync(int pageSize, int page)
        {
            var list = await _repProduct.GetPaginationAsync(pageSize, page);

            if (!list.Any())
                throw new NotFoundException("Lista de produto não encontrado");

            return list;
        }

        public async Task<List<Product>> GetProductAsync(List<Wish> listWishProducts)
        {
            if (!listWishProducts.Any())
                throw new NotContentException(string.Empty);

            List<Product> listProduct = new List<Product>();

            foreach (var wishProduct in listWishProducts)
            {
                Guid guid;
                if (!Guid.TryParse(wishProduct.IdProduct, out guid))
                    throw new ValidationException($"Produto com id {wishProduct.IdProduct} inválido");

                Product product = await _repProduct.GetById(guid);

                if (product == null)
                    throw new NotFoundException($"Produto com id {wishProduct.IdProduct} não encontrado");

                listProduct.Add(product);
            }

            return listProduct;
        }
    }
}
