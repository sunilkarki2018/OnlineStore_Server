using Ecommerce.Core.src.Entities;

namespace Ecommerce.Core.src.Abstractions
{
    public interface IUserRepo : IBaseRepo<User>
    {

        Task<User?> FindByEmailAsync(string email);
    }
}