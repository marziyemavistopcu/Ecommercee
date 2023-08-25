using Ecommerce.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Ecommerce.Model;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Caching.Memory;
using Ecommerce.Model.Views;
using Microsoft.AspNetCore.Authorization;
using StackExchange.Redis;
using EcommerceWebsite.Code.Validators;
using FluentValidation;

namespace EcommerceWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        public UsersController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
        {

        }
        [Authorize(Roles = "admin, personel")]
        [HttpGet("all-users")]
        public dynamic AllUsers()
        {
            
            List<Users> allUsers = repo.UsersRepository.FindAll().ToList<Users>();
            
            return new
            {
                success = true,
                data = allUsers
            };
        }

        [Authorize(Roles = "admin, personel")]
        [HttpGet("{id}")]
        public dynamic GetUserById(int id)
        {
            Users user = repo.UsersRepository.FindByCondition(a => a.Id == id).SingleOrDefault<Users>();

            return new
            {
                success = true,
                data = user
            }; 
        }

        [Authorize(Roles = "admin, personel")]
        [HttpGet("{username}/{password}")]
        public dynamic GetUserByUsername(string username, string password)
        {
            Users user = repo.UsersRepository.FindByCondition(a => a.username == username && a.password == password).SingleOrDefault<Users>();

            return new
            {
                success = true,
                data = user
            };
        }

        // [Throttle(Name = "Test Throttle", Seconds=5, Requests=5)] -- WebAPIThrottle.  == İstek kısıtlaması.
        [HttpPost("register")]
        public dynamic Register([FromBody] dynamic model)
        {
            
            dynamic jason = JObject.Parse(model.GetRawText());

            string username = jason.username;
            string password = jason.password;
            string email = jason.email;
            string first_name = jason.first_name;

            Users newUser = new Users()
            {
                username = username,
                password = password,
                email = email,
                first_name = first_name,
                role_id = Enums.Roles.üye
            };

            // Register olurken girilen kullanıcı adını DB de var mı diye kontrol et varsa hata mesajı yayınla.
            Users user = repo.UsersRepository.FindByCondition(u => u.username == newUser.username).SingleOrDefault<Users>();
            if (user != null) {
                return new
                {
                    success = false,
                    message = "Bu kullanıcı adı zaten alınmış."
                };
            }

            // Validatordaki validationsları kontrol et hata varsa fırlat.
            UserValidator validator = new UserValidator();
            validator.ValidateAndThrow(newUser);

            // Yeni kullanıcı oluştur ve DBye kayıt et.
            repo.UsersRepository.Create(newUser);
            repo.SaveChanges();

            return new
            {
                success = true
            };
        }

        [Authorize(Roles = "admin")] // Bu metodu kullanıcı girişi yapmış ve yetkili kullanıcılar görsün.
        [HttpGet("admins")]
        public dynamic GetAdmins()
        {
            List<V_Admins> item = repo.UsersRepository.BringAdminUsers();

            return new
            {
                success = true,
                data = item
            };
        }
    }
}
