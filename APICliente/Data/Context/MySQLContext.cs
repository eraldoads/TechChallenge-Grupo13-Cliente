using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Context
{
    public class MySQLContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public MySQLContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringMysql = _configuration.GetConnectionString("ConnectionMysql");
            optionsBuilder.UseMySql(connectionStringMysql, ServerVersion.AutoDetect(connectionStringMysql), builder => builder.MigrationsAssembly("APICliente"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração das entidades do modelo, incluindo chaves primárias, chaves estrangeiras e outros relacionamentos.
            modelBuilder.Entity<Cliente>().HasKey(c => c.IdCliente);
        }

        public DbSet<Cliente> Cliente { get; set; }
    }
}
