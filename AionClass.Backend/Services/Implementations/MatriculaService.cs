using AionClass.Backend.Data;
using AionClass.Backend.Models;
using AionClass.Backend.Services.Interfaces;
using System.Data.Entity;

namespace AionClass.Backend.Services.Implementations
{
    public class MatriculaService : IMatriculaService
    {
        private readonly ApplicationDbContext _context;

        public MatriculaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Matricula>> ObterTodasAsync()
        {
            return await _context.Matriculas
                .Include(m => m.ApplicationUser)
                .ToListAsync();
        }

        public async Task<Matricula> ObterPorIdAsync(int id)
        {
            return await _context.Matriculas
                .Include(m => m.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Matricula>> ObterPorUsuarioIdAsync(string usuarioId)
        {
            return await _context.Matriculas
                .Where(m => m.ApplicationUserId == usuarioId)
                .Include(m => m.ApplicationUser)
                .ToListAsync();
        }

        public async Task<Matricula> CriarAsync(Matricula matricula)
        {
            _context.Matriculas.Add(matricula);
            await _context.SaveChangesAsync();
            return matricula;
        }

        public async Task<Matricula> AtualizarAsync(int id, Matricula matriculaAtualizada)
        {
            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula == null)
                return null;

            matricula.Curso = matriculaAtualizada.Curso;
            matricula.DataMatricula = matriculaAtualizada.DataMatricula;
            matricula.ApplicationUserId = matriculaAtualizada.ApplicationUserId;

            await _context.SaveChangesAsync();
            return matricula;
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula == null)
                return false;

            _context.Matriculas.Remove(matricula);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UsuarioPossuiMatriculaAsync(string usuarioId, string curso)
        {
            return await _context.Matriculas.AnyAsync(m => m.ApplicationUserId == usuarioId && m.Curso == curso);
        }
    }
}
