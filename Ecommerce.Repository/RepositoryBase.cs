using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public abstract class RepositoryBase<T> where T : class // Set<T> has to be a class
    {
        //CRUD Operations

        protected RepositoryContext RepositoryContext { get; set; }
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll() => RepositoryContext.Set<T>().AsNoTracking(); // Bring every data from "T" Table.
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => RepositoryContext.Set<T>().Where(expression).AsNoTracking(); // Bring element by condition
        public void Create(T entity) => RepositoryContext.Set<T>().Add(entity); // Insert
        public void Update(T entity) => RepositoryContext.Set<T>().Update(entity); // Update 
        public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity); // Delete

    }
}
