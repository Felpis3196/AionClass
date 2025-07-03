using AionClass.Backend.Data;
using AionClass.Backend.Models;
using AionClass.Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AionClass.Backend.Services.Implementations
{
    public class CursoService : ICursoService
    {
        private readonly ApplicationDbContext _context;

        public CursoService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Curso> AtualizarAsync(int id, Curso cursoAtualizado)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
                return null;

            curso.Title = cursoAtualizado.Title;
            curso.Description = cursoAtualizado.Description;
            curso.Category = cursoAtualizado.Category;
            curso.Level = cursoAtualizado.Level;
            curso.ThumbnailUrl = cursoAtualizado.ThumbnailUrl;
            curso.IsActive = cursoAtualizado.IsActive;

            await _context.SaveChangesAsync();
            return curso;
        }

        public async Task<Curso> CriarAsync(Curso curso)
        {
            try
            {
                curso.CreatedAt = DateTime.SpecifyKind(curso.CreatedAt, DateTimeKind.Utc);
                _context.Cursos.Add(curso);
                await _context.SaveChangesAsync();
                return curso;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar o curso", ex);
            }
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
                return false;

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Curso> ObterPorIdAsync(int id)
        {
            return await _context.Cursos
               .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Curso>> ObterTodosAsync()
        {
            return await _context.Cursos
                .ToListAsync();
        }
    }
}
