using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Core.src.Paramters
{
    public class GetAllParams
    {
        public int Limit { get; set; } = 20;
        public int Offset { get; set; } = 0;
        public string Search { get; set; } = string.Empty;
        public Guid CategoryId { get; set; } // = new Guid()
    }
}