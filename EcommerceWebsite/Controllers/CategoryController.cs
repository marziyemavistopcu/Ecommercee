using Ecommerce.Model;
using Ecommerce.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;

namespace EcommerceWebsite.Controllers
{
    [Route("api/[controller]")] // localhost/api/category
    [ApiController]
    public class CategoryController : BaseController
    {
        public CategoryController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
        {

        }

        [HttpGet("all-categories")] // localhost/api/category/all-categories
        public dynamic AllCategories()
        {
            List<Category> categories;

            
            if(!cache.TryGetValue("all-categories", out categories))
            {
                categories = repo.CategoryRepository.FindAll().ToList<Category>();
                cache.Set("all-categories", categories, DateTimeOffset.UtcNow.AddMinutes(20));
            } 
            return new // Eğer tüm kategoriler cachce bellekte varsa direkt buraya atlayacak.
            {
                success = true,
                data = categories
            };
        }

        [HttpGet("{id}")]
        public dynamic Get(int id)
        {
            Category item = repo.CategoryRepository.FindByCondition(a => a.Id == id).SingleOrDefault<Category>();

            return new
            {
                success = true,
                data = item             
            };
        }

        [Authorize(Roles = "admin, personel")]
        [HttpPost("add-category")]
        public dynamic AddCategory([FromBody] dynamic model)
        {
            dynamic jason = JObject.Parse(model.GetRawText());

            Category newCategory = new Category()
            {
                Id = jason.Id,
                category_name = jason.category_name
            };

            if(newCategory.Id > 0)
            {
                repo.CategoryRepository.Update(newCategory);
            } else
            {
                repo.CategoryRepository.Create(newCategory);
            }

            repo.SaveChanges();

            cache.Remove("all-categories");

            return new
            {
                success = true
            };
        }

    }
}

