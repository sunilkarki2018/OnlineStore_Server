
using AutoMapper;
using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Business.src.Shared;
using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Paramters;

namespace Ecommerce.Business.src.Services
{
    public class UserService : IUserService
    {
        private IUserRepo _userRepo;
        private IMapper _mapper;

        public UserService(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public UserReadDTO CreateUser(UserCreateDTO userCreateDto)
        {
            var result = _userRepo.CreateOne(_mapper.Map<UserCreateDTO, User>(userCreateDto));
            return _mapper.Map<User, UserReadDTO>(result);
        }

        public string Login(string email, string password)
        {
            // call the repo: _repo.FindUserByCredential(email, password)
            // find user
            // encrypt user data into a token

            var user = new User()
            {
                Email = email,
                Password = password
            };
            return _userRepo.GenerateToken(user);

        }

        public bool DeleteUser(Guid id)
        {
            return _userRepo.DeleteOne(id);
        }

        public IEnumerable<UserReadDTO> GetAllUser(GetAllParams options)
        {
            var result = _userRepo.GetAll(options);
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserReadDTO>>(result);
        }

        public UserReadDTO GetUserById(Guid id)
        {
            var user = _userRepo.GetOneById(id);
            return _mapper.Map<User, UserReadDTO>(user);
        }
        public UserReadDTO? UpdateUser(UserUpdateDTO userUpdateDTO)
        {
            var existingUser = _userRepo.GetOneById(userUpdateDTO.Id);
            if (existingUser == null)
            {
                return null;
            }
            _mapper.Map<UserUpdateDTO, User>(userUpdateDTO, existingUser);
            return _mapper.Map<User, UserReadDTO>(_userRepo.UpdateOne(existingUser));
        }
    }
}