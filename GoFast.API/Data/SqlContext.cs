using GoFast.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GoFast.API.Data
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Motorista> Motorista { get; set; }
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<BlobStorage> BlobStorage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlContext).Assembly);
            //modelBuilder.Entity<Usuario>().HasKey(t => t.Id);
            //modelBuilder.Entity<Motorista>().HasKey(t => t.Id);
            //modelBuilder.Entity<Carro>().HasKey(t => t.Id);

            //modelBuilder.Entity<Documento>().HasKey(t => t.Id);
            //modelBuilder.Entity<Documento>().HasMany(b => b.BlobStorage)
            //    .With



            //modelBuilder.Entity<Endereco>().HasKey(t => t.Id);
            //modelBuilder.Entity<BlobStorage>().HasKey(t => t.Id);

        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
