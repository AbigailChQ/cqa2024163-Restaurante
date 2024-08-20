using Microsoft.EntityFrameworkCore;
using Restaurante.Models;

namespace Restaurante.Data
{
    public class RestauranteDbContext : DbContext
    {
        public RestauranteDbContext(DbContextOptions<RestauranteDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Comida> Comidas { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
         protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Orden>()
            .HasOne(o => o.Usuario)
            .WithMany(u => u.Ordenes)
            .HasForeignKey(o => o.UsuarioId);

            modelBuilder.Entity<Orden>()
                .HasOne(o => o.Comida)
                .WithMany(c => c.Ordenes)
                .HasForeignKey(o => o.ComidaId);
        }

    }
}
