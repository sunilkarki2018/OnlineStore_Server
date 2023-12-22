using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Business.src.Services;
using Ecommerce.Business.src.Shared;
using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Shared;
using Moq;

namespace Ecommerce.Test.src
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepo> _mockRepo;
        private static IMapper _mapper;

        public UserServiceTest()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(m =>
                {
                    m.AddProfile(new MapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public async void GetAllUsersAsync_ShouldInvokeRepoUsersMethod()
        {
            var userRepo = new Mock<IUserRepo>();
            var mapper = new Mock<IMapper>();
            var userService = new UserService(userRepo.Object, _mapper);
            GetAllOptions options = new GetAllOptions() { Limit = 5, Offset = 0 };

            await userService.GetAllAsync(options);

            userRepo.Verify(repo => repo.GetAllAsync(options), Times.Once);
        }

        [Theory]
        [ClassData(typeof(GetAllUsersData))]
        public async void GetAllUsersAsync_ShouldReturnValidResponse(IEnumerable<User> userResponse, IEnumerable<UserReadDTO> expected)
        {
            var userRepo = new Mock<IUserRepo>();
            GetAllOptions options = new GetAllOptions() { Limit = 5, Offset = 0 };
            userRepo.Setup(repo => repo.GetAllAsync(options)).ReturnsAsync(userResponse);
            var userService = new UserService(userRepo.Object, _mapper);

            var userReadDTOs = await userService.GetAllAsync(options);

            Assert.Equivalent(expected, userReadDTOs);
        }

        public class GetAllUsersData : TheoryData<IEnumerable<User>, IEnumerable<UserReadDTO>>
        {
            public GetAllUsersData()
            {
                User user1 = new User() { Id = Guid.NewGuid(), FirstName = "Jack", LastName = "Oliver" };
                User user2 = new User() { Id = Guid.NewGuid(), FirstName = "Jenny", LastName = "Swift" };
                User user3 = new User() { Id = Guid.NewGuid(), FirstName = "Mark", LastName = "Cena" };
                IEnumerable<User> users = new List<User>() { user1, user2, user3 };

                Add(users, _mapper.Map<IEnumerable<User>, IEnumerable<UserReadDTO>>(users));
            }
        }

        [Fact]
        public async void GetUserByIdAsync_ShouldInvokeRepoCategoryMethod()
        {
            var id = Guid.NewGuid();
            var userRepo = new Mock<IUserRepo>();
            var mapper = new Mock<IMapper>();
            var userService = new UserService(userRepo.Object, _mapper);

            await userService.GetByIdAsync(id);

            userRepo.Verify(repo => repo.GetByIdAsync(id), Times.Once);
        }

        [Theory]
        [ClassData(typeof(GetUserByIdData))]
        public async void GetCategoryByIdAsync_ShouldReturnValidResponse(User user, UserReadDTO expected, Type exceptionType)
        {
            var userRepo = new Mock<IUserRepo>();
            userRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user);
            var userService = new UserService(userRepo.Object, _mapper);

            var response = await userService.GetByIdAsync(It.IsAny<Guid>());

            Assert.Equivalent(expected, response);
        }

        public class GetUserByIdData : TheoryData<User?, UserReadDTO?, Type?>
        {
            public GetUserByIdData()
            {
                User user = new User() { FirstName = "Sunil", LastName = "Karki" };
                UserReadDTO userReadDTO = _mapper.Map<User, UserReadDTO>(user);
                Add(user, userReadDTO, null);
            }
        }

        [Fact]
        public async void CreateOneAsync_ShouldInvokeRepoMethod()
        {
            var userRepo = new Mock<IUserRepo>();
            var mapper = new Mock<IMapper>();
            var userService = new UserService(userRepo.Object, _mapper);
            UserCreateDTO userCreateDTO = new UserCreateDTO() { FirstName = "Sunil", LastName = "Karki", Password = "test123" };

            await userService.CreateOneAsync(userCreateDTO);

            userRepo.Verify(repo => repo.CreateOneAsync(It.IsAny<User>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(CreateUserData))]
        public async void CreateOneAsync_ShouldReturnValidResponse(User userRepoResponse, bool emailExist, UserReadDTO expected, Type? exceptionType)
        {
            var userRepo = new Mock<IUserRepo>();
            userRepo.Setup(repo => repo.CheckEmailExistAsync(It.IsAny<User>())).ReturnsAsync(emailExist);
            userRepo.Setup(repo => repo.CreateOneAsync(It.IsAny<User>())).ReturnsAsync(userRepoResponse);
            var userService = new UserService(userRepo.Object, _mapper);
            UserCreateDTO userCreateDTO = new UserCreateDTO() { FirstName = "Sunil", LastName = "Karki", Password = "test123", Email = "sunilkarki2018@gmail.com" };

            if (exceptionType is not null)
            {
                await Assert.ThrowsAsync(exceptionType, async () => await userService.CreateOneAsync(userCreateDTO));
            }
            else
            {
                var response = await userService.CreateOneAsync(userCreateDTO);
                Assert.Equivalent(expected, response);
            }
        }
        public class CreateUserData : TheoryData<User?, bool, UserReadDTO?, Type?>
        {
            public CreateUserData()
            {
                User user = new User() { FirstName = "Sunil", LastName = "Karki", Password = "test123", Email = "sunilkarki2018@gmail.com" };
                PasswordService.HashPassword(user.Password, out string hashPassword, out byte[] salt);
                user.Password = hashPassword;
                user.Salt = salt;
                UserReadDTO userReadDTO = _mapper.Map<User, UserReadDTO>(user);
                Add(user, false, userReadDTO, null);
                Add(null, true, null, typeof(CustomException));
            }
        }

        [Fact]
        public async void DeleteUserByIdAsync_ShouldInvokeRepoUserMethod()
        {
            var id = Guid.NewGuid();
            User user = new User() { Id = id, FirstName = "Sunil", LastName = "Karki" };
            var userRepo = new Mock<IUserRepo>();
            var mapper = new Mock<IMapper>();
            userRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user);
            userRepo.Setup(repo => repo.CheckEmailExistAsync(It.IsAny<User>())).ReturnsAsync(false);

            var userService = new UserService(userRepo.Object, _mapper);

            await userService.DeleteOneAsync(id);

            userRepo.Verify(repo => repo.DeleteOneAsync(user), Times.Once);
        }

        [Theory]
        [ClassData(typeof(DeleteUserData))]
        public async void DeleteCategoryAsync_ShouldReturnValidResponse(Guid id)
        {
            User user = new User() { Id = id, FirstName = "Sunil", LastName = "Karki" };
            var userRepo = new Mock<IUserRepo>();
            var mapper = new Mock<IMapper>();
            userRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user);
            userRepo.Setup(repo => repo.DeleteOneAsync(It.IsAny<User>())).ReturnsAsync(true);
            var userService = new UserService(userRepo.Object, _mapper);

            var response = await userService.DeleteOneAsync(It.IsAny<Guid>());

            Assert.Equivalent(true, response);
        }
        public class DeleteUserData : TheoryData<Guid>
        {
            public DeleteUserData()
            {
                var id = Guid.NewGuid();
                Add(id);
            }
        }

        [Fact]
        public async void UpdateUserByIdAsync_ShouldInvokeRepoUserMethod()
        {
            User user = new User() { Id = Guid.NewGuid(), FirstName = "Sunil", LastName = "Karki" };
            var id = Guid.NewGuid();
            var userRepo = new Mock<IUserRepo>();
            var mapper = new Mock<IMapper>();
            userRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user);
            var userService = new UserService(userRepo.Object, _mapper);
            UserUpdateDTO userUpdateDTO = new UserUpdateDTO() { FirstName = "Sunil", LastName = "Karki" };

            await userService.UpdateOneAsync(id, userUpdateDTO);

            userRepo.Verify(repo => repo.UpdateOneAsync(It.IsAny<User>()), Times.Once);
        }


                        [Theory]
                [ClassData(typeof(UpdateUserData))]
                public async void UpdateOneAsync_ShouldReturnValidResponse(User user,UserUpdateDTO userUpdateDTO, bool expected, Type? exceptionType)
                {
                    //UserUpdateDTO userUpdateDTO = new UserUpdateDTO() { FirstName = "Sunil", LastName = "Karki" };
                    var userRepo = new Mock<IUserRepo>();
                     userRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user);
                    userRepo.Setup(repo => repo.UpdateOneAsync(It.IsAny<User>())).ReturnsAsync(true);
                    var userService = new UserService(userRepo.Object, _mapper);

                    var response = await userService.UpdateOneAsync(Guid.NewGuid(), userUpdateDTO);
                    Assert.Equivalent(expected, response);

                }
                public class UpdateUserData : TheoryData<User,UserUpdateDTO, bool, Type?>
                {
                    public UpdateUserData()
                    {
                        User user = new User() { FirstName = "Sunil", LastName = "Karki" };                         
                        UserUpdateDTO userUpdateDTO = new UserUpdateDTO() { FirstName = "Sunil", LastName = "Karki" };
                        Add(user,userUpdateDTO, true, null);
                    }
                }

    }
}