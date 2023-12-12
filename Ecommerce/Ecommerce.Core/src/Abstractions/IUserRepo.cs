using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Paramters;

namespace Ecommerce.Core.src.Abstractions
{
    public interface IUserRepo
    {
        IEnumerable<User> GetAll(GetAllParams options);
        User? GetOneById(Guid id);
        User CreateOne(User user);
        User UpdateOne(User user);
        bool DeleteOne(Guid id);
        string GenerateToken(User user);
    }
}