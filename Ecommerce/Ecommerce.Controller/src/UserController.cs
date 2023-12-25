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
        public async Task<ActionResult<PaginatedUserReadDTO>> GetPaginatedUsersAsync([FromQuery] GetAllOptions getAllOptions)
        {
            return Ok(await _service.GetAllPaginatedUserDTOAsync(getAllOptions));
        }
        [Authorize(Roles = "Admin")]
        public override Task<ActionResult<bool>> UpdateOneAsync([FromRoute] Guid id, [FromBody] UserUpdateDTO updateObject)
        {
            return base.UpdateOneAsync(id, updateObject);
        }
        [Authorize(Roles = "Admin")]
        public override Task<ActionResult<bool>> DeleteOneAsync([FromRoute] Guid id)
        {
            return base.DeleteOneAsync(id);
        }
    }
}