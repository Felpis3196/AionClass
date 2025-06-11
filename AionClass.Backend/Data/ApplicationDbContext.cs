using AionClass.Backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AionClass.Backend.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<Curso> Cursos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Matricula>()
               .HasOne(m => m.Curso)
               .WithMany(c => c.Matriculas)
               .HasForeignKey(m => m.CursoId);

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
