
using AutoMapper;
using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Business.src.Shared;
using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Shared;

namespace Ecommerce.Business.src.Services
{
    public class UserService : BaseService<User, UserReadDTO, UserCreateDTO, UserUpdateDTO>, IUserService
    {
        protected readonly IUserRepo _repo;
        public UserService(IUserRepo repo, IMapper mapper) : base(repo, mapper)
        {
            _repo = repo;
        }
        public async Task<bool> UpdatePasswordAsync(string newPassword, Guid userId)
        {
            var user = await _repo.GetByIdAsync(userId);
            if (user is null)
            {
                throw CustomException.NotFoundException("User not found");
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
            if (await _repo.CheckEmailExistAsync(user))
            {
                throw CustomException.DuplicateEmailException("Email already exist");
            };
            return _mapper.Map<User, UserReadDTO>(await _repo.CreateOneAsync(user));
        }
        public override async Task<bool> UpdateOneAsync(Guid id, UserUpdateDTO updateObject)
        {
            var existingUser = await _repo.GetByIdAsync(id);
            if (existingUser is null)
            {
                throw CustomException.NotFoundException("User not found");
            }
            _mapper.Map<UserUpdateDTO, User>(updateObject, existingUser);
            if (await _repo.CheckEmailExistAsync(existingUser))
            {
                throw CustomException.DuplicateEmailException("Email already exist");
            };
            return await _repo.UpdateOneAsync(existingUser);
        }
        public async Task<PaginatedUserReadDTO> GetAllPaginatedUserDTOAsync(GetAllOptions getAllOptions)
        {
            return new PaginatedUserReadDTO()
            {
                Users = _mapper.Map<IEnumerable<User>, IEnumerable<UserReadDTO>>(await _repo.GetAllAsync(getAllOptions)),
                PageCount = Math.Ceiling((decimal)await _repo.GetUserRecordCountAsync(getAllOptions) / getAllOptions.Limit)
            };
        }


    }
}