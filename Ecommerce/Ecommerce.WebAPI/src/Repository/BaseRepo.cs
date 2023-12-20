using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Shared;
using Ecommerce.WebAPI.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.WebAPI.src.Repository
{
    public class BaseRepo<T> : IBaseRepo<T> where T : BaseEntity
    {
        protected readonly DbSet<T> _data;
        protected readonly DatabaseContext _databaseContext;
        public BaseRepo(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _data = _databaseContext.Set<T>();
        }
        public async Task<T> CreateOneAsync(T createObject)
        {
            await _data.AddAsync(createObject);
            await _databaseContext.SaveChangesAsync();
            return createObject;
        }

        public async Task<bool> DeleteOneAsync(T deleteObject)
        {
            if (await _data.AsNoTracking().FirstOrDefaultAsync(e => e.Id == deleteObject.Id) is null)
            {
                return false;
            }

            _data.Remove(deleteObject);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(GetAllOptions getAllOptions)
        {
            return await _data.AsNoTracking().Skip(getAllOptions.Offset).Take(getAllOptions.Limit).ToArrayAsync();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _data.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> UpdateOneAsync(T updateObject)
        {
            if (await _data.AsNoTracking().FirstOrDefaultAsync(e => e.Id == updateObject.Id) is null)
            {
                return false;
            }
            _data.Update(updateObject);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}