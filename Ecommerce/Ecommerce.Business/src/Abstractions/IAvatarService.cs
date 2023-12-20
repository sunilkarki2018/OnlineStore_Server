using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.src.Abstractions
{
    public interface IAvatarService 
        {
        Task<string> CreateAvatarAsync(AvatarCreateDTO avatarCreateDTO);
        Task<string> GetAvatarByUserIdAsync(Guid userId);
    }
}
