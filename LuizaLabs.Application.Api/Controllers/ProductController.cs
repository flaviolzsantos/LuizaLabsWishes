﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LuizaLabs.Domain.Entities;
using LuizaLabs.Domain.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuizaLabs.Application.Api.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ISrvProduct _srvProduct;

        public ProductController(ISrvProduct srvProduct)
        {
            _srvProduct = srvProduct;
        }

        [HttpGet]
        public async Task<List<Product>> GetProductrAsync(int page_size, int page)
        {
            return await _srvProduct.GetPaginationAsync(page_size, page);
        }

        [HttpPost]
        public async Task CreateProductAsync(Product product)
        {
            await _srvProduct.AddAsync(product);
            Response.StatusCode = (int)HttpStatusCode.Created;
        }
    }
}
