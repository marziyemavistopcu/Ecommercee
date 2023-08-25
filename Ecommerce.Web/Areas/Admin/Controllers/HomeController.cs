using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Rol() => View();
        public IActionResult Product() => View();   
        public IActionResult Category() => View();  
    }
}
