using AionClass.Backend.Data;
using AionClass.Backend.Models;
using AionClass.Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AionClass.Backend.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationUser>> ObterTodosAsync()
        {
            // Lista todos os usuários
            return await _context.Users
                .Include(u => u.Matriculas) // se quiser carregar as matrículas junto
                .ToListAsync();
        }

        public async Task<ApplicationUser> ObterPorIdAsync(string id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<ApplicationUser> AtualizarAsync(string id, ApplicationUser usuarioAtualizado)
        {
            var usuario = await _context.Users.FindAsync(id);
            if (usuario == null)
                return null;

            usuario.PrimeiroNome = usuarioAtualizado.PrimeiroNome;
            usuario.Sobrenome = usuarioAtualizado.Sobrenome;
            usuario.Avatar = usuarioAtualizado.Avatar;

            if (usuarioAtualizado.UltimoLogin.HasValue)
            {
                usuario.UltimoLogin = DateTime.SpecifyKind(usuarioAtualizado.UltimoLogin.Value, DateTimeKind.Utc);
            }

            usuario.EstaAtivo = usuarioAtualizado.EstaAtivo;
            usuario.Role = usuarioAtualizado.Role;
            usuario.PerfilUsuario = usuarioAtualizado.PerfilUsuario;

            usuario.UserName = usuarioAtualizado.UserName;
            usuario.Email = usuarioAtualizado.Email;
            usuario.PhoneNumber = usuarioAtualizado.PhoneNumber;

            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<ApplicationUser> CriarAsync(ApplicationUser novoUsuario)
        {
            _context.Users.Add(novoUsuario);
            await _context.SaveChangesAsync();
            return novoUsuario;
        }

        public async Task<bool> DeletarAsync(string id)
        {
            var usuario = await _context.Users.FindAsync(id);
            if (usuario == null)
                return false;

            _context.Users.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
