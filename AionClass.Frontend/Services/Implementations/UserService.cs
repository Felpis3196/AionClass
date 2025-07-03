using AionClass.Frontend.Data;
using AionClass.Frontend.Models;
using AionClass.Frontend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AionClass.Frontend.Services.Implementations
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
            try
            {
                return await _context.Users
                    .Include(u => u.Matriculas)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter a lista de usuários.", ex);
            }
        }

        public async Task<ApplicationUser> ObterPorIdAsync(string id)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar usuário por ID.", ex);
            }
        }

        public async Task<ApplicationUser> AtualizarAsync(string id, ApplicationUser usuarioAtualizado)
        {
            try
            {
                var usuario = await _context.Users.FindAsync(id);
                if (usuario == null)
                    return null;

                usuario.PrimeiroNome = usuarioAtualizado.PrimeiroNome;
                usuario.Sobrenome = usuarioAtualizado.Sobrenome;
                usuario.Avatar = usuarioAtualizado.Avatar;
                usuario.UltimoLogin = usuarioAtualizado.UltimoLogin;
                usuario.EstaAtivo = usuarioAtualizado.EstaAtivo;
                usuario.Role = usuarioAtualizado.Role;
                usuario.PerfilUsuario = usuarioAtualizado.PerfilUsuario;

                usuario.UserName = usuarioAtualizado.UserName;
                usuario.Email = usuarioAtualizado.Email;
                usuario.PhoneNumber = usuarioAtualizado.PhoneNumber;

                await _context.SaveChangesAsync();
                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar usuário.", ex);
            }
        }

        public async Task<ApplicationUser> CriarAsync(ApplicationUser novoUsuario)
        {
            try
            {
                _context.Users.Add(novoUsuario);
                await _context.SaveChangesAsync();
                return novoUsuario;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar usuário.", ex);
            }
        }

        public async Task<bool> DeletarAsync(string id)
        {
            try
            {
                var usuario = await _context.Users.FindAsync(id);
                if (usuario == null)
                    return false;

                _context.Users.Remove(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar usuário.", ex);
            }
        }
    }
}
