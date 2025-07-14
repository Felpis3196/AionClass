using AionClass.Backend.Models;
using AionClass.Backend.Models.DTO;

namespace AionClass.Backend.Services.Interfaces
{
    public interface IMatriculaService
    {
        Task<IEnumerable<Matricula>> ObterTodasAsync();
        Task<Matricula> ObterPorIdAsync(int id);
        Task<IEnumerable<MatriculaCursoDTO>> ObterPorUsuarioIdAsync(string usuarioId);
        Task<Matricula> CriarAsync(Matricula matricula);
        Task<Matricula> AtualizarAsync(int id, Matricula matriculaAtualizada);
        Task<bool> DeletarAsync(int id);
        Task<bool> UsuarioPossuiMatriculaAsync(string usuarioId, string curso);
    }
}
