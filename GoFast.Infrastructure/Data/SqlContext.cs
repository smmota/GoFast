using GoFast.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoFast.Infrastructure.Data
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Motorista> Motorista { get; set; }
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Endereco> Endereco { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
