using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Test.Entities;

namespace Test.Repository
{
    public class JwtServices : IJwtServices
    {
        private readonly Jwt _option;

        public JwtServices(Jwt option)
        {
            _option = option;
        }
        public string CreateToken(User user)
        {
            //var x = new JwtSecurityToken(
            //    issuer: _option.Issuer,
            //    audience: _option.Audience,
            //    signingCredentials:
            //    Claim: 
            //);
            //return new JwtSecurityTokenHandler().WriteToken(x);

            var TokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _option.Issuer,
                Audience = _option.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_option.SigningKey)), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.IsAdmin? "admin": "user")
                })
            };
            var securitytoken = TokenHandler.CreateToken(tokenDescriptor);
            var token = TokenHandler.WriteToken(securitytoken);
            return token;
        }
    }
}
