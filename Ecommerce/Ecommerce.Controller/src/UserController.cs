using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controller.src
{
    public class UserController : BaseController<User, UserReadDTO, UserCreateDTO, UserUpdateDTO>
    {
        protected readonly IUserService _service;
        public UserController(IUserService service, IAvatarService avatarService) : base(service)
        {
            _service = service;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("paginated-users")]
        public async Task<ActionResult<PaginatedUserReadDTO>> GetPaginatedUsersAllAsync([FromQuery] GetAllOptions getAllOptions)
        {
            return Ok(await _service.GetAllPaginatedUserDTOAsync(getAllOptions));
        }
    }
}