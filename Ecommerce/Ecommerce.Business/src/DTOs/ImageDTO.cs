﻿using Ecommerce.Core.src.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.src.DTOs
{
    public class ImageCreateDTO
    {
        public Guid ProductLineId { get; set; }
        public byte[] Data { get; set; }
    }
    public class ImageReadDTO
    {
        public Guid Id { get; set; }
        public Guid ProductLineId { get; set; }
        public byte[] Data { get; set; }
    }
    public class ImageUpdateDTO
    {
        public Guid Id { get; set; }
        public Guid ProductLineId { get; set; }
        public byte[] Data { get; set; }
    }
}
