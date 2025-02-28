using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Test.Entities;

namespace Test.Repository
{
    public class HelperRepository : IHelperRepository
    {
        private readonly Jwt _option;

        public HelperRepository(Jwt option)
        {
            _option = option;
        }
        public string CreateToken(User user)
        {
            var TokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _option.Issuer,
                Audience = _option.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_option.SiningKey)), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                })
            };
            var securitytoken = TokenHandler.CreateToken(tokenDescriptor);
            var token = TokenHandler.WriteToken(securitytoken);
            return token;
        }
    }
}
