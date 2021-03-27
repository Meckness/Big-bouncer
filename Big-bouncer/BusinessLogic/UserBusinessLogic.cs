using Big_bouncer.Data;
using Big_bouncer.Models;
using Big_bouncer.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Big_bouncer.BusinessLogic
{
    public class UserBusinessLogic
    {
        private readonly UserRepository _userRepository;
        private readonly ITokenBuilder _tokenBuilder;
        public UserBusinessLogic(UserRepository userRepository, ITokenBuilder tokenBuilder)
        {
            _userRepository = userRepository;
            _tokenBuilder = tokenBuilder;
        }
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userRepository.GetUsersAsync();
        }
        public async Task<User> GetUserAsync(string userName)
        {
            return await _userRepository.GetUserAsync(userName);
        }
        public async Task<User> AddUserAsync(User user)
        {
            //Verify first if User Already Exist, then add
            var result = await _userRepository.AddUserAsync(new User()
            {
                Id = new Guid(),
                Username = user.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            });
            return result.Entity;
        }
        public void UpdateUser(User user)
        {
            _userRepository.UpdateUser(user);
        }
        public void DeleteUser(User user)
        {
            _userRepository.DeleteUser(user);
        }
        public async Task<bool> VerifyUserExistance(Claim userClaim)
        {
            return await _userRepository.VerifyUserExistance(userClaim);
        }
        public async Task Save()
        {
            await _userRepository.Save();
        }

    }
}
