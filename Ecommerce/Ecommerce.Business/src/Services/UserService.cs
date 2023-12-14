
using AutoMapper;
using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Business.src.Shared;
using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;

namespace Ecommerce.Business.src.Services
{
    public class UserService : BaseService<User, UserReadDTO, UserCreateDTO, UserUpdateDTO>, IUserService
    {
        public UserService(IUserRepo repo, IMapper mapper) : base(repo, mapper)
        {
        }
        public async Task<bool> UpdatePasswordAsync(string newPassword, Guid userId)
        {
            var user = await _repo.GetByIdAsync(userId);
            if (user is null)
            {
                throw new Exception();
            }
            PasswordService.HashPassword(newPassword, out string hashedPassword, out byte[] salt);
            user.Password = hashedPassword;
            user.Salt = salt;
            return await _repo.UpdateOneAsync(user);
        }
        public override async Task<UserReadDTO> CreateOneAsync(UserCreateDTO createObject)
        {
            PasswordService.HashPassword(createObject.Password, out string hashPassword, out byte[] salt);
            var user = _mapper.Map<UserCreateDTO, User>(createObject);
            user.Password = hashPassword;
            user.Salt = salt;
            return _mapper.Map<User, UserReadDTO>(await _repo.CreateOneAsync(user));

        }

    }
}