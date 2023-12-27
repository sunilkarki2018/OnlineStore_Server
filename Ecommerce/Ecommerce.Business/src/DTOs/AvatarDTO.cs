using Ecommerce.Core.src.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.src.DTOs
{
    public class AvatarReadDTO : BaseEntityDTO
    {
        public UserReadDTO UserReadDTO { get; set; }
        public byte[] Data { get; set; }
    }
    public class AvatarCreateDTO
    {
        public Guid UserId { get; set; }
        public byte[] Data { get; set; }
    }
    public class AvatarUpdateDTO : BaseEntityDTO
    {
        public Guid UserId { get; set; }
        public byte[] Data { get; set; }
    }
}
