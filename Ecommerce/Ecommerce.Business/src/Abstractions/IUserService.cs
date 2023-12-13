using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Paramters;

namespace Ecommerce.Business.src.Abstractions
{
    public interface IUserService
    {
        IEnumerable<UserReadDTO> GetAllUser(GetAllParams options);
        UserReadDTO GetUserById(Guid id);
        UserReadDTO CreateUser(UserCreateDTO userCreateDto);
        UserReadDTO? UpdateUser(UserUpdateDTO userUpdateDto);
        bool DeleteUser(Guid id);
        string Login(string email, string password);
    }
}