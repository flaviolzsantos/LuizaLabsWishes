using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LuizaLabs.Domain.Entities;
using LuizaLabs.Domain.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuizaLabs.Application.Api.Controllers
{
    [Authorize]
    [Route("wishes")]
    [ApiController]
    public class WhishController : ControllerBase
    {
        private readonly ISrvWish _srvWish;

        public WhishController(ISrvWish srvWish)
        {
            _srvWish = srvWish;
        }

        [HttpPost("{userId}")]
        public async Task CreateWishes(List<Wish> listWishProducts, string userId)
        {
            await _srvWish.CreateWishesAsync(listWishProducts, userId);
            Response.StatusCode = (int)HttpStatusCode.Created;
        }

        [HttpGet("{userId}")]
        public async Task<IEnumerable<Product>> GetWishesAsync(int page_size, int page, string userId)
        {
            return await _srvWish.GetPaginationAsync(page_size, page, userId);
        }
    }
}