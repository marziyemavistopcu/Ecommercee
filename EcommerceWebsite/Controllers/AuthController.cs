using Ecommerce.Model;
using Ecommerce.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerceWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        public AuthController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
        {

        }

        [HttpPost("Login")]
        public dynamic Login([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());

            string username = json.username;
            string password = json.password;

            var user = repo.UsersRepository.FindByCondition(u => u.username == username && u.password == password).SingleOrDefault();

            if (user != null)
            {
                // Get current users role
                Role role = repo.RoleRepository.FindByCondition(r => r.Id == user.role_id).SingleOrDefault<Role>();

                Dictionary<string, object> claims = new Dictionary<string, object>();  
                
                if (role != null)
                {
                    claims.Add(ClaimTypes.Role, role.role);
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes("JAS(DJA()+^)(%/^()JKL3428942343kl4j234(U)+Û849dgfgre");

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
                    Claims = claims
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new
                {
                    success = true,
                    data = tokenHandler.WriteToken(token),
                    rol = role.role
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "Incorrect Username or Password"
                };
            }
        }
    }
}