using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Business.src.Services;
using Ecommerce.Core.src.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controller.src
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvatarController
    {

        protected readonly IAvatarService _service;
        public AvatarController(IAvatarService service)
        {
            _service = service;
        }

        [HttpPost("upload-avatar")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<string>> UploadAvatar([FromForm] UserForm userForm)
        {
            if (userForm.AvatarImage == null || userForm.AvatarImage.Length == 0)
            {
                throw new Exception("avatar is missing");
            }
            else
            {
                using (var ms = new MemoryStream())
                {
                    await userForm.AvatarImage.CopyToAsync(ms);
                    var content = ms.ToArray();
                    var avatar = new AvatarCreateDTO { Data = content, UserId = userForm.UserId };
                    return await _service.CreateAvatarAsync(avatar);
                    //return BitConverter.ToString(content);
                }
            }
        }

        [HttpGet("get-avatar")]
        public async Task<ActionResult<string>> GetAvatarById([FromQuery] string userId)
        {
            return await _service.GetAvatarByUserIdAsync(Guid.Parse(userId));
        }
    }

    public class UserForm
    {
        public IFormFile AvatarImage { get; set; }
        public Guid UserId { get; set; }
    }

}
