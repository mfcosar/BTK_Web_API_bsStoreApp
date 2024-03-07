using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryContext : IdentityDbContext<User>  //DbContext idi
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }
        //Artuk elimizde DbContext var, hayırlı olsun :)
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        //migr.script type config edilmezse yani BookConfig. verilmezse boş gelir:
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); //ilgili tablolar oluşur

            //model oluşturulurken bu Config ifadesi dikkate alınacak
            /*modelBuilder.ApplyConfiguration(new BookConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());*/

            //tek tek yazmak yerine assembly üzerinden alınabilir:
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
