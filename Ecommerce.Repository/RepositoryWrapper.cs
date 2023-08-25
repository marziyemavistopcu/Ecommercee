using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public class RepositoryWrapper
    {
        private RepositoryContext context;

        private CategoryRepository categoryRepository;
        private UsersRepository usersRepository;
        private ProductRepository productRepository;
        private RoleRepository roleRepository;

        public RepositoryWrapper(RepositoryContext context)
        {
            this.context = context;   
        }

        public CategoryRepository CategoryRepository
        {
            get{ 
                if(categoryRepository == null)    
                    categoryRepository = new CategoryRepository(context); 

                return categoryRepository;
            }
        }

        public UsersRepository UsersRepository
        {
            get 
            { 
                if(usersRepository == null)
                    usersRepository = new UsersRepository(context);
                return usersRepository;
            }
        }

        public ProductRepository ProductRepository
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepository(context);
                return productRepository;
            }
        }

        public RoleRepository RoleRepository
        {
            get
            {
                if(roleRepository == null)
                    roleRepository = new RoleRepository(context);
                return roleRepository;
            }
        }

        // Ekleninenlerin DB ye eklenmesi için
        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
