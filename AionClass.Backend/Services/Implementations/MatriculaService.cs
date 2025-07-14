using AionClass.Backend.Data;
using AionClass.Backend.DTOs;
using AionClass.Backend.Models;
using AionClass.Backend.Models.DTO;
using AionClass.Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                .ToListAsync();
        }

        public async Task<Matricula> ObterPorIdAsync(int id)
        {
            return await _context.Matriculas
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<MatriculaCursoDTO>> ObterPorUsuarioIdAsync(string usuarioId)
        {
            return await _context.Matriculas
                .Include(m => m.Curso)
                .Where(m => m.ApplicationUserId == usuarioId)
                .Select(m => new MatriculaCursoDTO
                {
                    Id = m.Id,
                    DataMatricula = m.DataMatricula,
                    Curso = new CursoDto
                    {
                        Id = m.Curso.Id,
                        Title = m.Curso.Title,
                        Description = m.Curso.Description,
                        Category = m.Curso.Category,
                        Level = m.Curso.Level,
                        ThumbnailUrl = m.Curso.ThumbnailUrl
                    }
                })
                .ToListAsync();
        }


        public async Task<Matricula> CriarAsync(Matricula matricula)
        {
            bool jaMatriculado = await _context.Matriculas
                .AnyAsync(m => m.ApplicationUserId == matricula.ApplicationUserId && m.CursoId == matricula.CursoId);

            if (jaMatriculado)
                throw new InvalidOperationException("Usuário já matriculado neste curso.");

            matricula.DataMatricula = matricula.DataMatricula.ToUniversalTime();
            _context.Matriculas.Add(matricula);
            await _context.SaveChangesAsync();
            return matricula;
        }

        public async Task<Matricula> AtualizarAsync(int id, Matricula matriculaAtualizada)
        {
            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula == null)
                return null;

            matricula.CursoId = matriculaAtualizada.CursoId;
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

        public async Task<bool> UsuarioPossuiMatriculaAsync(string usuarioId, string cursoId)
        {
            return await _context.Matriculas
                .AnyAsync(m => m.ApplicationUserId == usuarioId && m.CursoId.ToString() == cursoId.ToString());
        }
    }
}
 