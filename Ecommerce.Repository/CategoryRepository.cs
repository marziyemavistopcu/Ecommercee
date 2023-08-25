using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public class CategoryRepository : RepositoryBase<Category>
    {
        public CategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        //public List<Category> AnaSayfaKategorileriniGetir() { 

        //    List<Category> homePageCategories =  (from k in RepositoryContext.Categories
        //     join a in RepositoryContext.HomePageCategories on k.Id equals a.CategoryId
        //     select k).ToList<Category>();

        //    return homePageCategories;
        //}
   
    }
}
