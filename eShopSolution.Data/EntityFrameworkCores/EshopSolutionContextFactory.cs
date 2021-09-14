using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace eShopSolution.Data.EntityFrameworkCores
{
    public class EshopSolutionContextFactory : IDesignTimeDbContextFactory<EShopSolutionDbContext>
    {
        public EshopSolutionContextFactory()
        {
        }

        public EShopSolutionDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot cfg = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetting.json")
                .Build();
            var connectionString = cfg.GetConnectionString("Default");
            var optionsBuilder = new DbContextOptionsBuilder<EShopSolutionDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new EShopSolutionDbContext(optionsBuilder.Options);
        }
    }
}
