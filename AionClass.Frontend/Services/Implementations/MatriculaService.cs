using AionClass.Frontend.Data;
using AionClass.Frontend.Models;
using AionClass.Frontend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AionClass.Frontend.Services.Implementations
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
            try
            {
                return await _context.Matriculas.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter a lista de matrículas.", ex);
            }
        }

        public async Task<Matricula> ObterPorIdAsync(int id)
        {
            try
            {
                return await _context.Matriculas.FirstOrDefaultAsync(m => m.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar matrícula por ID.", ex);
            }
        }

        public async Task<IEnumerable<Matricula>> ObterPorUsuarioIdAsync(string usuarioId)
        {
            try
            {
                return await _context.Matriculas
                    .Where(m => m.ApplicationUserId == usuarioId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar matrículas do usuário.", ex);
            }
        }

        public async Task<Matricula> CriarAsync(Matricula matricula)
        {
            try
            {
                _context.Matriculas.Add(matricula);
                matricula.DataMatricula = matricula.DataMatricula.ToUniversalTime();
                await _context.SaveChangesAsync();
                return matricula;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar matrícula.", ex);
            }
        }

        public async Task<Matricula> AtualizarAsync(int id, Matricula matriculaAtualizada)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar matrícula.", ex);
            }
        }

        public async Task<bool> DeletarAsync(int id)
        {
            try
            {
                var matricula = await _context.Matriculas.FindAsync(id);
                if (matricula == null)
                    return false;

                _context.Matriculas.Remove(matricula);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar matrícula.", ex);
            }
        }

        public async Task<bool> UsuarioPossuiMatriculaAsync(string usuarioId, string curso)
        {
            try
            {
                return await _context.Matriculas
                    .AnyAsync(m => m.ApplicationUserId == usuarioId && m.Curso.Title == curso);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao verificar matrícula existente para o usuário.", ex);
            }
        }
    }
}
