using AutoMapper;
using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Business.src.Shared;
using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.src.Services
{
    public class AvatarService : IAvatarService
    {
        private readonly IAvatarRepo _avatarRepo;
        private readonly IMapper _mapper;
        public AvatarService(IAvatarRepo avatarRepo, IMapper mapper)
        {
            _avatarRepo = avatarRepo;
            _mapper = mapper;
        }
        public async Task<string> CreateAvatarAsync(AvatarCreateDTO avatarCreateDTO)
        {
            return await _avatarRepo.CreateAvatarAsync(_mapper.Map<AvatarCreateDTO, Avatar>(avatarCreateDTO));
        }

        public async Task<string> GetAvatarByUserIdAsync(Guid userId)
        {
            var data = await _avatarRepo.GetAvatarByUserIdAsync(userId);
            if (data == null)
            {
                throw CustomException.NotFoundException("Avatar not found");
            }
            return data;
        }
    }
}
