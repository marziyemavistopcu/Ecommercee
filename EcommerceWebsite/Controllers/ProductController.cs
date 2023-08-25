using Ecommerce.Model;
using Ecommerce.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using System.Linq;


namespace EcommerceWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {

        public ProductController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
        {
            
        }

        [HttpGet("all-products/{categoryId}")]
        public dynamic ProductsByCategoryId(int categoryId)
        {
            List<Product> products = repo.ProductRepository.AllProductsByCategoryId(categoryId);

            return new
            {
                success = true,
                data = products
            };
        }

        [HttpGet("all-products")]
        public dynamic GetAllProducts()
        {
            List<Product> allProducts;

            // Bring allProducts from cache.
            if(!cache.TryGetValue("all-products", out allProducts))
            {
                allProducts = repo.ProductRepository.FindAll().ToList<Product>();
                cache.Set("all-products", allProducts, DateTimeOffset.UtcNow.AddMinutes(20));
            }
            
            return new
            {
                success = true,
                data = allProducts
            };
        }

        [HttpGet("{id}")]
        public dynamic GetProductById(int id)
        {
            Product product = repo.ProductRepository.FindByCondition(a => a.Id == id).SingleOrDefault<Product>();

            return new
            {
                success = true,
                data = product
            };
        }

        //[Authorize(Roles = "admin, personel")]
        [HttpPost("add-product")]
        public dynamic AddItem([FromBody] dynamic model)
        {
            try
            {
                dynamic jason = JObject.Parse(model.GetRawText());

                Product product = new Product()
                {
                    Id = jason.Id,
                    title = jason.title,
                    product_description = jason.product_description,
                    price = jason.price,
                    quantity = jason.quantity,
                    in_stock = jason.in_stock,
                    category_id = jason.category_id,
                    img = jason.img
                };

                if (jason.Id > 0)
                    repo.ProductRepository.Update(product);
                else
                    repo.ProductRepository.Create(product);

                repo.SaveChanges(); // Databaseye eklemek için yapmamız gerekiyor.

                return new
                {
                    success = true,
                };

            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = ex.Message
                };
            }
        }

        //[Authorize(Roles = "admin")]
        [HttpDelete("delete")]
        public dynamic DeleteProduct(int id)
        {
            if(id <= 0)
            {
                return new
                {
                    success = false,
                    message = "Geçersiz Id"
                };
            }
            repo.ProductRepository.DeleteItem(id);

            return new
            {
                success = true
            };
        }

    }
}
