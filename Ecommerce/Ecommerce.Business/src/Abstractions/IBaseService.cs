using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Shared;

namespace Ecommerce.Business.src.Abstractions
{
    public interface IBaseService<T, TReadDTO, TCreateDTO, TUpdateDTO>
    where T : BaseEntity
    {
        Task<IEnumerable<TReadDTO>> GetAllAsync(GetAllOptions getAllOptions);
        Task<TReadDTO?> GetByIdAsync(Guid Id);
        Task<bool> UpdateOneAsync(Guid id,TUpdateDTO updateObject);
        Task<bool> DeleteOneAsync(Guid Id);
        Task<TReadDTO> CreateOneAsync(TCreateDTO createObject);
    }
}