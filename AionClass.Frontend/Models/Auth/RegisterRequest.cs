namespace AionClass.Frontend.Models.Auth
{
    public class RegisterRequest
    {
        public string PrimeiroNome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string? Avatar { get; set; }
        public string Password { get; set; }
        public string PerfilUsuario { get; set; } // Student | Instructor | Admin
        public string Role { get; set; } // ex: "Admin"
    }
}
