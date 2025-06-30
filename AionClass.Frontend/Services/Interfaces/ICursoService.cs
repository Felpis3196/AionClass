using AionClass.Frontend.Models;

namespace AionClass.Frontend.Services.Interfaces
{
    public interface ICursoService
    {
        Task<IEnumerable<Curso>> ObterTodosAsync();
        Task<Curso> ObterPorIdAsync(int id);
        Task<Curso> CriarAsync(Curso curso);
        Task<Curso> AtualizarAsync(int id, Curso curso);
        Task<bool> DeletarAsync(int id);
    }
}
