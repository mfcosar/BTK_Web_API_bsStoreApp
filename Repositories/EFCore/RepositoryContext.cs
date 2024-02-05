using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }
        //Artuk elimizde DbContext var, hayırlı olsun :)
        public DbSet<Book> Books { get; set; }

        //migr.script type config edilmezse yani BookConfig. verilmezse boş gelir:
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //model oluşturulurken bu Config ifadesi dikkate alınacak
            modelBuilder.ApplyConfiguration(new BookConfig());
        }
    }
}
