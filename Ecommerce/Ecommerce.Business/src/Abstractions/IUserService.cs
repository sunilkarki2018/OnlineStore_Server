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
        IEnumerable<UserReadDTO> GetAll(GetAllParams options);
        UserReadDTO GetOneById(Guid id);
        UserReadDTO CreateOne(UserCreateDTO userCreateDto);
        UserReadDTO? UpdateOne(UserUpdateDTO userUpdateDto);
        bool DeleteOne(Guid id);
        string Login(string email, string password);
    }
}