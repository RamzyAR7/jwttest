using Microsoft.AspNetCore.Mvc;
using Test.DTOs;
using Test.Entities;

namespace Test.Services
{
    public interface IUserServices
    {
        User Register(UserDto request);
        string Login(UserDto request);
        IList<User> GetUsers();
    }
}
