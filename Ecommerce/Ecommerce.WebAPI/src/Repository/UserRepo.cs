using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Shared;
using Ecommerce.WebAPI.src.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.WebAPI.src.Repository
{
    public class UserRepo : BaseRepo<User>, IUserRepo
    {
        public UserRepo(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
        public async Task<User?> FindByEmailAsync(string email)
        {
            return await _data.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }

       /*  public Task<bool> UpdatePasswordAsync(string newPassword, Guid userId)
        {
            throw new NotImplementedException();
        } */

        /*    private DbSet<User> _users;
  private DatabaseContext _database;
  private IConfiguration _config;

  public UserRepo(DatabaseContext database, IConfiguration config)
  {
      _users = database.Users;
      _database = database;
      _config = config;
  }
  public User CreateOne(User user)
  {
      _users.Add(user);
      _database.SaveChanges();
      return user;
  }
  public User UpdateOne(User user)
  {
      _users.Update(user);
      _database.SaveChanges();
      return user;
  }

  public bool DeleteOne(Guid id)
  {
      var user = _users.Where(x => x.Id == id).FirstOrDefault();
      if (user is null)
      {
          return false;
      }
      _users.Remove(user);
      var result = _database.SaveChanges();
      return true;
  }

  public IEnumerable<User> GetAll(GetAllOptions options)
  {
      return _users.Where(u => u.FirstName.Contains(options.Search)).Skip(options.Offset).Take(options.Limit);
  }

  public string GenerateToken(User user)
  {
      var claims = new List<Claim>{
          new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
          new Claim(ClaimTypes.Role, user.Role.ToString())
      };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
          _config.GetSection("Jwt:Key").Value!));

      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

      var token = new JwtSecurityToken(
              claims: claims,
              expires: DateTime.Now.AddDays(1),
              signingCredentials: creds
          );

      var jwt = new JwtSecurityTokenHandler().WriteToken(token);

      return jwt;
  }

  public string GenerateTokenBackUp(User user)
  {
      var issuer = _config.GetSection("Jwt:Issuer").Value;
      var claims = new List<Claim>{
          new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
          new Claim(ClaimTypes.Role, user.Role.ToString())
      };
      var audience = _config.GetSection("Jwt:Audience").Value;
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value!));
      var signingKey = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
      var descriptor = new SecurityTokenDescriptor
      {
          Issuer = issuer,
          Audience = audience,
          Expires = DateTime.Now.AddDays(2),
          Subject = new ClaimsIdentity(claims),
          SigningCredentials = signingKey
      };
      var token = tokenHandler.CreateToken(descriptor);
      return token.ToString()!;
  }

  public User? GetOneById(Guid id)
  {
      return _users.Where(u => u.Id == id).FirstOrDefault();
  }
*/

    }

}