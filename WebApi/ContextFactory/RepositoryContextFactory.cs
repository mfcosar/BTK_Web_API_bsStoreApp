﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repositories.EFCore;

namespace WebApi.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            //configurationBuilder: appsettings.json dosyasına ulaşır
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.
                GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            //DbContextOptionsBuilder : sqlConnection'a ulaşır
            var builder = new DbContextOptionsBuilder<RepositoryContext>().
                UseSqlServer(configuration.GetConnectionString("sqlConnection"),
                prj => prj.MigrationsAssembly("WebApi"));


            return new RepositoryContext(builder.Options);
        }
    }
}
