using AionClass.Backend.Models;

namespace AionClass.Backend.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> ObterTodosAsync();
        Task<ApplicationUser> ObterPorIdAsync(string id);
        Task<bool> DeletarAsync(string id);
        Task<ApplicationUser> AtualizarAsync(string id, ApplicationUser userAtualizado);
        Task<ApplicationUser> CriarAsync(ApplicationUser novoUsuario);
    }
}
