using Big_bouncer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Big_bouncer.Data
{
    public class UserRepository
    {
        private ApplicationDbContext _applicationDbContext;
        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _applicationDbContext.Users.ToListAsync();
        }
        public async Task<User> GetUserAsync(string userName)
        {
            return await _applicationDbContext.Users.SingleOrDefaultAsync(u => u.Username == userName);
        }
        public async ValueTask<EntityEntry<User>> AddUserAsync(User user)
        {
            return await _applicationDbContext.AddAsync(new User()
            {
                Id = new Guid(),
                Username = user.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
            });
        }
        public void UpdateUser(User user)
        {
           _applicationDbContext.Entry(user).State = EntityState.Modified;
        }
        public void DeleteUser(User user)
        {
            _applicationDbContext.Remove(user);
        }
        public async Task<bool> VerifyUserExistance(Claim userClaim)
        {
            return await _applicationDbContext.Users.AnyAsync(u => u.Username == userClaim.Value);
        }
        public async void Save()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
