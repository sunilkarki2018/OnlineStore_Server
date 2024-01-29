using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Shared;
using Ecommerce.WebAPI.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.WebAPI.src.Repository
{
    public class UserRepo : BaseRepo<User>, IUserRepo
    {
        public UserRepo(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<bool> CheckEmailExistAsync(User user)
        {
            return await _data.AsNoTracking().AnyAsync(u => u.Email == user.Email && u.Id != user.Id);
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
            return await _data.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }

        public override async Task<User?> GetByIdAsync(Guid id)
        {
            var res = await _data.AsNoTracking().Include(u => u.Address).Include(u => u.Avatar).AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            return await _data.AsNoTracking().Include(u => u.Address).Include(u => u.Avatar).AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public override async Task<IEnumerable<User>> GetAllAsync(GetAllOptions getAllOptions)
        {
            return await _data.AsNoTracking().Include(u => u.Address).Include(u => u.Avatar).Where(u => u.FirstName.Contains(getAllOptions.SearchKey) || u.LastName.Contains(getAllOptions.SearchKey)).Skip(getAllOptions.Offset).Take(getAllOptions.Limit).ToArrayAsync();
        }

        public async Task<int> GetUserRecordCountAsync(GetAllOptions getAllOptions)
        {
            return await _data.Where(u => u.FirstName.Contains(getAllOptions.SearchKey) || u.LastName.Contains(getAllOptions.SearchKey)).CountAsync();
        }

    }

}