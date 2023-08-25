using Ecommerce.Model;
using Ecommerce.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;

namespace EcommerceWebsite.Controllers
{
    // [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {

        public RoleController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
        {

        }

        [HttpGet("all-roles")]
        public dynamic AllRoles()
        {
            List<Role> allRoles = repo.RoleRepository.FindAll().ToList<Role>();

            return new
            {
                success = true,
                data = allRoles
            };
        }

        [HttpPost("add-role")]
        public dynamic AddRole([FromBody] dynamic model)
        {
            try
            {
                dynamic json = JObject.Parse(model.GetRawText());

                Role role = new Role()
                {
                    Id = json.Id,
                    role = json.role
                };

                if (json.Id > 0)
                {
                    repo.RoleRepository.Update(role);
                }
                else
                {
                    repo.RoleRepository.Create(role);
                }

                repo.SaveChanges(); // Ekleninen rolün DB ye eklenmesi için

                return new
                {
                    success = true
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = ex.Message,
                };
            }
        }

        [HttpDelete("delete")]
        public dynamic DeleteRoleById(int id)
        {
            if (id <= 0)
            {
                return new
                {
                    success = false,
                    message = "Geçersiz id"
                };
            }
            repo.RoleRepository.DeleteRole(id);
            return new
            {
                success = true
            };
        }
    }
}