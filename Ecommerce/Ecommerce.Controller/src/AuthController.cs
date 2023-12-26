using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
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
        private readonly IUserService _userService;
        public AuthController(IAuthService service, IUserService userService)
        {
            _service = service;
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost()]
        public async Task<string> Login([FromBody] Credentials credentials)
        {
            return await _service.Login(credentials);
        }
        [Authorize]
        [HttpGet("get-profile")]
        public async Task<UserReadDTO?> GetProfile()
        {
            var authenticatedClaims = HttpContext.User;
            var userId = authenticatedClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

            return await _userService.GetByIdAsync(new Guid(userId));
        }
    }
}