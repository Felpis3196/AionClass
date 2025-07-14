using AionClass.Frontend.Models;

namespace AionClass.Frontend.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> ObterTodosAsync();
        Task<ApplicationUser> ObterPorIdAsync(string id);
        Task<bool> DeletarAsync(string id);
        Task<ApplicationUser> AtualizarAsync(string id, ApplicationUser userAtualizado);
        Task<ApplicationUser> CriarAsync(ApplicationUser novoUsuario);
        Task<UserPerfilViewModel> ObterPerfilAsync();
    }
}
