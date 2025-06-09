using System.ComponentModel.DataAnnotations;

namespace AionClass.Backend.DTOs.Auth
{
    public class RegisterRequest
    {
        [Required]
        public string PrimeiroNome { get; set; }

        [Required]
        public string Sobrenome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Url]
        public string Avatar { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; }

        [Required]
        [RegularExpression("Student|Instructor|Admin", ErrorMessage = "O perfil deve ser 'Student', 'Instructor' ou 'Admin'.")]
        public string PerfilUsuario { get; set; }
    }

}
