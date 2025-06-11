using System;

namespace AionClass.Backend.Models
{
    public class Matricula
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
        public DateTime DataMatricula { get; set; }
        public string ApplicationUserId { get; set; }
    }


}
