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

            // Configuração da relação ApplicationUser -> Matriculas (1:N)
            builder.Entity<Matricula>()
                .HasOne(m => m.ApplicationUser)
                .WithMany(u => u.Matriculas)
                .HasForeignKey(m => m.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

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
