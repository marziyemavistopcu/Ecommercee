using Ecommerce.Web.Code;
using Ecommerce.Web.Code.Rest;
using Ecommerce.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login() => View();

        public IActionResult GirisYap(LoginModel model)
        {
            UserRestClient client = new UserRestClient();
            dynamic result = client.Login(model.Username, model.Password);
            bool success = result.success;

            if(success)
            {
                Repo.Session.Username = model.Username;
                Repo.Session.Token = (string)result.data;
                Repo.Session.Role = (string)result.rol;

                return RedirectToAction("Index", "Home");
            } 
            else
            {
                ViewBag.LoginError = result.message;
                return View("Login");
            }   
        }
    }
}
