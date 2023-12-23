using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Business.src.Abstractions;
using Ecommerce.Core.src.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controller.src
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        [HttpPost()]
        public async Task<string> Login([FromBody] Credentials credentials)
        {
            return await _service.Login(credentials);
        }
    }
}