using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Paramters;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controller.src.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<UserReadDTO>> GetAll([FromQuery] GetAllParams options)
        {
            return Ok(_userService.GetAll(options));
        }
        [HttpGet("{id}")]
        public ActionResult<UserReadDTO> GetById([FromRoute] Guid id)
        {
            return Ok(_userService.GetOneById(id));
        }

        [HttpPost()]
        public ActionResult<UserReadDTO> CreateOne([FromBody] UserCreateDTO userCreateDto)
        {
            return CreatedAtAction(nameof(CreateOne), _userService.CreateOne(userCreateDto));
        }

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] User user)
        {
            return Ok(_userService.Login(user.Email, user.Password));
        }

        [HttpPut("{id}")]
        public ActionResult<UserUpdateDTO> UpdateUser(Guid id, [FromBody] UserUpdateDTO userUpdateDTO)
        {
            if (id != userUpdateDTO.Id)
            {
                return BadRequest("The provided ID does not match the user ID.");
            }
            _userService.UpdateOne(userUpdateDTO);
            return Ok("User updated successfully");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(Guid id)
        {
            _userService.DeleteOne(id);
            return Ok("User deleted successfully");
        }
    }
}