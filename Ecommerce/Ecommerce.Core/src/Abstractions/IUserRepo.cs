using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Shared;

namespace Ecommerce.Core.src.Abstractions
{
    public interface IUserRepo : IBaseRepo<User>
    {
        Task<User?> FindByEmailAsync(string email);
        Task<int> GetUserRecordCountAsync(GetAllOptions getAllOptions);

        Task<bool> CheckEmailExistAsync(User user);
    }
}