using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;
using Ecommerce.WebAPI.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.WebAPI.src.Repository
{
    public class AvatarRepo : BaseRepo<Avatar>, IAvatarRepo
    {
        public AvatarRepo(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
        public async Task<string> CreateAvatarAsync(Avatar avatar)
        {
            await _data.AddAsync(avatar);
            await _databaseContext.SaveChangesAsync();
            return avatar.Id.ToString();
        }

        public async Task<string?> GetAvatarByUserIdAsync(Guid userId)
        {
            var avatar = await _data.FirstOrDefaultAsync(x => x.UserId == userId);
            if (avatar == null)
                return null;
            return BitConverter.ToString(avatar.Data);
        }
    }
}
