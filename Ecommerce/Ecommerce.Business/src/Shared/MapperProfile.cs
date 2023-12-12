using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Entities;

namespace Ecommerce.Business.src.Shared
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserReadDTO>().ForMember(dest => dest.FirstName, option => option.MapFrom(source => source.FirstName.ToLower()));
            CreateMap<UserCreateDTO, User>();
            CreateMap<UserUpdateDTO, User>();
        }
    }
}