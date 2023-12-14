using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Shared;

namespace Ecommerce.Core.src.Abstractions
{
    public interface IBaseRepo<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync(GetAllOptions getAllOptions);
        Task<T?> GetByIdAsync(Guid Id);
        Task<bool> UpdateOneAsync(T updateObject);
        Task<bool> DeleteOneAsync(T deleteObject);
        Task<T> CreateOneAsync(T createObject);
    }
}