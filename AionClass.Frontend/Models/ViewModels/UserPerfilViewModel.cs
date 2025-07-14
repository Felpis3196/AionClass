using AionClass.Frontend.Models.ViewModels;

namespace AionClass.Frontend.Models.DTO
{
    public class UserPerfilViewModel
    {
        public string Id { get; set; }
        public string PrimeiroNome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }
        public List<MatriculaViewModel> Matricula { get; set; }
    }
}