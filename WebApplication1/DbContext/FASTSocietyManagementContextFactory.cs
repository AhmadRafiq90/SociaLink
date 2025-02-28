using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WebApplication1.DBcontext
{
    public class FASTSocietyManagementContextFactory : IDesignTimeDbContextFactory<FASTSocietyManagementContext>
    {
        private FASTSocietyManagementContext context;

        public FASTSocietyManagementContextFactory()
        {
            context = CreateDbContext(Array.Empty<string>());
        }
        public FASTSocietyManagementContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<FASTSocietyManagementContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            context = new FASTSocietyManagementContext(optionsBuilder.Options);
            return context;
        }
        public FASTSocietyManagementContext GetContext()
        {             
            return context;
        }
    }
}
