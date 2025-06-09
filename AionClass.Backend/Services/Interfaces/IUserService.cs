using AionClass.Backend.Models;

namespace AionClass.Backend.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> ObterTodosAsync();
        Task<ApplicationUser> ObterPorIdAsync(int id);
        Task<bool> DeletarAsync(int id);
        Task<ApplicationUser> AtualizarAsync(int id, ApplicationUser userAtualizado);
        Task<ApplicationUser> CriarAsync(ApplicationUser novoUsuario);
    }
}
