using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Shared;

namespace Ecommerce.Business.src.Abstractions
{
    public interface IUserService : IBaseService<User, UserReadDTO, UserCreateDTO, UserUpdateDTO>
    {
        Task<PaginatedUserReadDTO> GetAllPaginatedUserDTOAsync(GetAllOptions getAllOptions);

        Task<bool> UpdatePasswordAsync(string newPassword, Guid userId);
    }
}