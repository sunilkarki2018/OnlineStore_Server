using Ecommerce.Core.src.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.src.Abstractions
{
    public interface IAvatarRepo : IBaseRepo<Avatar>
    {
        public Task<string> CreateAvatarAsync(Avatar avatar);
        public Task<string?> GetAvatarByUserIdAsync(Guid userId);
    }
}
