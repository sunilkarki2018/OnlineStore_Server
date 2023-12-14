using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Business.src.Abstractions;
using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Shared;

namespace Ecommerce.Business.src.Services
{
    public class BaseService<T, TReadDTO, TCreateDTO, TUpdateDTO> : IBaseService<T, TReadDTO, TCreateDTO, TUpdateDTO>
    where T : BaseEntity
    {
        protected readonly IBaseRepo<T> _repo;
        protected readonly IMapper _mapper;
        public BaseService(IBaseRepo<T> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public virtual async Task<TReadDTO> CreateOneAsync(TCreateDTO createObject)
        {
            return _mapper.Map<T, TReadDTO>(await _repo.CreateOneAsync(_mapper.Map<TCreateDTO, T>(createObject)));
        }

        public virtual async Task<bool> DeleteOneAsync(Guid Id)
        {
            return await _repo.DeleteOneAsync(await _repo.GetByIdAsync(Id));
        }

        public virtual async Task<IEnumerable<TReadDTO>> GetAllAsync(GetAllOptions getAllOptions)
        {
            return _mapper.Map<IEnumerable<T>, IEnumerable<TReadDTO>>(await _repo.GetAllAsync(getAllOptions));
        }

        public virtual async Task<TReadDTO?> GetByIdAsync(Guid Id)
        {
            return _mapper.Map<T?, TReadDTO?>(await _repo.GetByIdAsync(Id));
        }

        public virtual async Task<bool> UpdateOneAsync(Guid id, TUpdateDTO updateObject)
        {
            return await _repo.UpdateOneAsync(_mapper.Map<TUpdateDTO, T>(updateObject));
        }
    }
}