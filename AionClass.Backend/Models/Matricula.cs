using System;

namespace AionClass.Backend.Models
{
    public class Matricula
    {
        public int Id { get; set; }
        public string Curso { get; set; }
        public DateTime DataMatricula { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }

}
