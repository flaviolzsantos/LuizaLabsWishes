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
                throw new ValidationException("Paylod requerido");

            if(string.IsNullOrEmpty(product.Name))
                throw new ValidationException("Nome requerido");

            if (await _repProduct.HasName(product.Name))
                throw new AlreadyExistException("Não foi possível cadastrar esse produto pois ele já está cadastrado");

            await _repProduct.Add(product);
        }

        public async Task<List<Product>> GetPaginationAsync(int pageSize, int page)
        {
            var list = await _repProduct.GetPaginationAsync(pageSize, page);

            if (!list.Any())
                throw new NotContentException();

            return list;
        }
    }
}
