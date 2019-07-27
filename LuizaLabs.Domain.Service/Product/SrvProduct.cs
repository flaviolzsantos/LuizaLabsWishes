using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LuizaLabs.Domain.Entities;
using LuizaLabs.Infra.Cross;
using LuizaLabs.Infra.Data.Interfaces;
using LuizaLabs.Infra.Data.Repository;

namespace LuizaLabs.Domain.Service
{
    public class SrvProduct : ISrvProduct
    {
        private readonly IRepProduct _repProduct;
        public SrvProduct(IRepProduct repProduct)
        {
            _repProduct = repProduct;
        }

        public async Task AddAsync(Product obj)
        {
            if (obj == null)
                throw new ValidationException("Paylod requerido");

            if(string.IsNullOrEmpty(obj.Name))
                throw new ValidationException("Nome requerido");

            await _repProduct.Add(obj);
        }

        public async Task<List<Product>> GetPaginationAsync(int pageSize, int page)
        {
            return await _repProduct.GetPaginationAsync(pageSize, page);
        }
    }
}
