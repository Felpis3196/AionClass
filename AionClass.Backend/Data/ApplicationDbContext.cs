using AionClass.Backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AionClass.Backend.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Matricula> Matriculas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Restringe o tamanho de Role e PerfilUsuario (opcional)
            builder.Entity<ApplicationUser>()
                .Property(u => u.Role)
                .HasMaxLength(50);

            builder.Entity<ApplicationUser>()
                .Property(u => u.PerfilUsuario)
                .HasMaxLength(50);
        }
    }
}
