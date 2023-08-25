using Ecommerce.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public class RoleRepository : RepositoryBase<Role>
    {
        public RoleRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public void DeleteRole(int roleId)
        {
            RepositoryContext.Roles.Where(r => r.Id == roleId).ExecuteDelete();
        }
    }
}
