using AionClass.Frontend.Models;

namespace AionClass.Frontend.Services.Interfaces
{
    public interface IMatriculaService
    {
        Task<IEnumerable<Matricula>> ObterTodasAsync();
        Task<Matricula> ObterPorIdAsync(int id);
        Task<IEnumerable<Matricula>> ObterPorUsuarioIdAsync(string usuarioId);
        Task<Matricula> CriarAsync(Matricula matricula);
        Task<Matricula> AtualizarAsync(int id, Matricula matriculaAtualizada);
        Task<bool> DeletarAsync(int id);
        Task<bool> UsuarioPossuiMatriculaAsync(string usuarioId, string cursoid);
    }
}
