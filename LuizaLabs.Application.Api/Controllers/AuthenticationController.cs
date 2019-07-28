using LuizaLabs.Domain.Entities;
using LuizaLabs.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace LuizaLabs.Application.Api.Controllers
{
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