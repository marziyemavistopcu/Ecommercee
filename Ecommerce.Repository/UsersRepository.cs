using Ecommerce.Model;
using Ecommerce.Model.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public class UsersRepository : RepositoryBase<Users>
    {
        public UsersRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public List<V_Admins> BringAdminUsers()
        {
            return RepositoryContext.Admins.ToList<V_Admins>();
        }
    }
}
