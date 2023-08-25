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
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; // CategoryRepo SQL sorgusunda max 32 döngü kabul eder. Sonsuz döngüden çıkmak için bunu yazıyoruz.
        }


        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Eğer viewsda primary key yoksa 
            // modelBuilder.Entity<V_Admins>().HasNoKey();
            modelBuilder.Entity<Product>().Property(a => a.in_stock).HasDefaultValue();
        }

        // Bu collection sayesinde veritabanına erişiliyor.
        public DbSet<Category> Categories { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<V_Admins> Admins {get; set;}

    }
}
