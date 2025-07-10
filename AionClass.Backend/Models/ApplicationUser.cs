using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AionClass.Backend.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string PrimeiroNome { get; set; }

        [Required]
        [StringLength(100)]
        public string Sobrenome { get; set; }

        [Display(Name = "Nome Completo")]
        public string NomeCompleto => $"{PrimeiroNome} {Sobrenome}";

        [Display(Name = "URL do Avatar")]
        [Url]
        public string? Avatar { get; set; }

        [Display(Name = "Data de Criação")]
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        [Display(Name = "Último Login")]
        public DateTime? UltimoLogin { get; set; }

        [Display(Name = "Está Ativo")]
        public bool EstaAtivo { get; set; } = true;

        [Required]
        [StringLength(50)]
        public string Role { get; set; }

        [Required]
        [Display(Name = "Perfil do Usuário")]
        [RegularExpression("Student|Instructor|Admin", ErrorMessage = "O perfil deve ser 'Student', 'Instructor' ou 'Admin'.")]
        public string PerfilUsuario { get; set; }
        public ICollection<Matricula>? Matriculas { get; set; }
    }
}
