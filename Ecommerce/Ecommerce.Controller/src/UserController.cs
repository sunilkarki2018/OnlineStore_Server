using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Business.src.Services;
using Ecommerce.Business.src.Shared;
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
        [HttpGet("create-users")]
        public new async Task<ActionResult<UserReadDTO>> CreateUserAsync([FromForm] UserCreateForm userCreateForm)
        {
            var userCreateDTO = new UserCreateDTO();

            if (userCreateDTO.AvatarCreateDTO == null)
            {
                throw CustomException.NotFoundException("avatar is missing");
            }
            else
            {
                userCreateDTO.FirstName = userCreateForm.FirstName;
                userCreateDTO.LastName = userCreateForm.LastName;
                userCreateDTO.Email = userCreateForm.Email;
                userCreateDTO.AddressCreateDTO = userCreateForm.AddressCreateDTO; 
                userCreateDTO.AvatarCreateDTO = userCreateForm.AvatarCreateDTO;
                /*
                foreach (var item in productLineCreateForm.Images)
                {
                    using (var ms = new MemoryStream())
                    {
                        await item.CopyToAsync(ms);
                        var content = ms.ToArray();
                        var productImage = new ImageCreateDTO { Data = content };
                        productLineCreateDTO.ImageCreateDTOs.Add(productImage);
                    }
                }
                */
            }
            return CreatedAtAction(nameof(CreateUserAsync), await _service.CreateOneAsync(userCreateDTO));
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
    public class UserCreateForm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public AvatarCreateDTO AvatarCreateDTO { get; set; }
        public AddressCreateDTO AddressCreateDTO { get; set; }
    }

}