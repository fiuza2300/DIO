using curso.api.Infraestruture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
namespace curso.api.Configurations
{
    public class DbFactoryDbContext : IDesignTimeDbContextFactory<CursoDbContext>
    {
        private readonly IConfiguration _configuration;

        public DbFactoryDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public CursoDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CursoDbContext>();
            optionsBuilder.UseSqlServer(_configuration.GetSection("JwtConfigurations:Secret").Value);
            var contexto = new CursoDbContext(optionsBuilder.Options);

            return contexto;
        }
    }
}
