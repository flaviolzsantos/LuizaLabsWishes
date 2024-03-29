﻿using LuizaLabs.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuizaLabs.Domain.Service
{
    public interface ISrvProduct : ISrvBase<Product>
    {
        Task<List<Product>> GetProductAsync(List<Wish> listWishProducts);
    }
}
