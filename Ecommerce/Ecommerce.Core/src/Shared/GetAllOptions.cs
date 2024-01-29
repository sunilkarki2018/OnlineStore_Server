using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Core.src.Shared
{
    public class GetAllOptions
    {
        public int Limit { get; set; } = 50;
        public int Offset { get; set; } = 0;
        public string? SearchKey { get; set; } = string.Empty;
    }
}