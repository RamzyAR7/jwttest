using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Contexts;
using Test.DTOs;
using Test.Entities;
using Test.Repository;

namespace Test.Services
{
    public class UserServices: IUserServices
    {
        private readonly UserDbContext _dbcontext;
        private readonly IJwtServices _helper;
        public UserServices(UserDbContext dbcontect, IJwtServices helper)
        {
            _dbcontext = dbcontect;
            _helper = helper;
        }
        public User? Register(UserDto request)
        {
            if (_dbcontext.Users.Any(u => u.UserName == request.UserName))
            {
                return null;
            }
            var user = new User
            {
                UserName = request.UserName
            };

            var hashedPassword = new PasswordHasher<User>()
                .HashPassword(user, request.Password);

            user.PasswordHash = hashedPassword;

            _dbcontext.Users.Add(user);
            _dbcontext.SaveChanges();
            return user;
        }
        public string? Login(UserDto request)
        {
            var user = _dbcontext.Users.FirstOrDefault(u => u.UserName == request.UserName);
            if (user is null)
            {
                return null;
            }
            // VerifyHashedPassword -> enum of type PasswordVerificationResult
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password)
                == PasswordVerificationResult.Success)
            {
                string token = _helper.CreateToken(user);
                return token;
            }
            else
            {
                return null;
            }
        }
        public IList<User> GetUsers()
        {
            var users = _dbcontext.Users.ToList();
            return users;
        }
    }
}
