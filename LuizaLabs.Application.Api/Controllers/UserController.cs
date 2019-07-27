using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LuizaLabs.Domain.Entities;
using LuizaLabs.Domain.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuizaLabs.Application.Api.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISrvUser _srvUser;

        public UserController(ISrvUser srvUser)
        {
            _srvUser = srvUser;
        }

        [HttpGet]
        public async Task<List<User>> GetUserAsync(int page_size, int page)
        {
            return await _srvUser.GetPaginationAsync(page_size, page);
        }

        [HttpPost]
        public async Task CreateUserAsync(User user)
        {
            await _srvUser.AddAsync(user);
        }
    }
}