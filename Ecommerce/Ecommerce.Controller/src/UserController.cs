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
        protected readonly IUserService _userService;
        public UserController(IUserService userService, IAvatarService avatarService) : base(userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("paginated-users")]
        public async Task<ActionResult<PaginatedUserReadDTO>> GetPaginatedUsersAsync([FromQuery] GetAllOptions getAllOptions)
        {
            return Ok(await _userService.GetAllPaginatedUserDTOAsync(getAllOptions));
        }

        [HttpPost("create-users")]
        [Consumes("multipart/form-data")]
        public new async Task<ActionResult<UserReadDTO>> CreateUserAsync([FromForm] UserCreateForm userCreateForm)
        {
            var userCreateDTO = new UserCreateDTO();

            if (userCreateForm.Avatar == null)
            {
                throw CustomException.NotFoundException("avatar is missing");
            }
            else
            {
                userCreateDTO.FirstName = userCreateForm.FirstName;
                userCreateDTO.LastName = userCreateForm.LastName;
                userCreateDTO.Email = userCreateForm.Email;
                userCreateDTO.Password = userCreateForm.Password;

                using (var ms = new MemoryStream())
                {
                    await userCreateForm.Avatar!.CopyToAsync(ms);
                    var content = ms.ToArray();
                    var userAvatar = new AvatarCreateDTO { Data = content };
                    userCreateDTO.Avatar.Data = userAvatar.Data;
                }
                userCreateDTO.Address.HouseNumber = userCreateForm.HouseNumber;
                userCreateDTO.Address.Street = userCreateForm.Street;
                userCreateDTO.Address.PostCode = userCreateForm.PostCode;
                userCreateDTO.Address.City = userCreateForm.City;
                userCreateDTO.Address.Country = userCreateForm.Country;

            }
            return CreatedAtAction(nameof(CreateUserAsync), await _userService.CreateOneAsync(userCreateDTO));
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
        public override async Task<ActionResult<IEnumerable<UserReadDTO>>> GetAllAsync([FromQuery] GetAllOptions getAllOptions)
        {
            var userReadDTOs = await _userService.GetAllAsync(getAllOptions);
            var result = ConvertImageDataToBase64(userReadDTOs.ToList());
            return Ok(result);
        }

        private List<UserReadDTO> ConvertImageDataToBase64(List<UserReadDTO> userReadDTOs)
        {
            foreach (var userReadDTO in userReadDTOs)
            {
                if (userReadDTO.Avatar != null)
                {
                    if (userReadDTO.Avatar.Data != null)
                    {
                        userReadDTO.Avatar.AvatarBase64Value = Convert.ToBase64String(userReadDTO.Avatar.Data);
                    }
                }
            }
            return userReadDTOs;
        }


    }
    public class UserCreateForm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile? Avatar { get; set; }
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        //public AvatarCreateDTO AvatarCreateDTO { get; set; }
        //public AddressCreateDTO AddressCreateDTO { get; set; }
    }

}