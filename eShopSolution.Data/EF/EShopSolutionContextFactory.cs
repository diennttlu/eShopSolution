using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace eShopSolution.Data.EF
{
    public class EShopSolutionContextFactory : IDesignTimeDbContextFactory<EShopSolutionDbContext>
    {
        public EShopSolutionDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            var optionsBuilder = new DbContextOptionsBuilder<EShopSolutionDbContext>();
            var connectionStrings = configuration.GetConnectionString("eShopSolutionDatabase");
            optionsBuilder.UseSqlServer(connectionStrings);

            return new EShopSolutionDbContext(optionsBuilder.Options);

        }
    }
}
