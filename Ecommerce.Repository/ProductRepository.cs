using Ecommerce.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public class ProductRepository : RepositoryBase<Product>
    {
        public ProductRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public List<Product> AllProductsByCategoryId(int categoryId)
        {
            List<Product> allProducts = (from p in RepositoryContext.Products
                                         join c in RepositoryContext.Categories on p.category_id equals c.Id
                                         where c.Id == categoryId
                                         select p).ToList<Product>();

            return allProducts;
        }

        public void DeleteItem(int productId)
        {
            RepositoryContext.Products.Where(a => a.Id == productId).ExecuteDelete();
        }
    }
}
