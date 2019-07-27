using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LuizaLabs.Domain.Entities;
using LuizaLabs.Domain.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuizaLabs.Application.Api.Controllers
{
    [Authorize]
    [Route("auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ISrvAuthentication _srvAuthentication;

        public AuthenticationController(ISrvAuthentication srvAuthentication)
        {
            _srvAuthentication = srvAuthentication;
        }

        [HttpPost]
        public IActionResult Authentication(Auth auth)
        {
            return Ok(_srvAuthentication.Authentication(auth));
        }
    }
}