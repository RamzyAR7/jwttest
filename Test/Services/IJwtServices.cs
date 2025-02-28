using Test.Entities;

namespace Test.Repository
{
    public interface IJwtServices
    {
        string CreateToken(User user);
    }
}
