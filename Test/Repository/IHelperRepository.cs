using Test.Entities;

namespace Test.Repository
{
    public interface IHelperRepository
    {
        string CreateToken(User user);
    }
}
