using System;

namespace AionClass.Frontend.Models
{
    public class Matricula
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
        public DateTime DataMatricula { get; set; } = DateTime.Now;
        public string ApplicationUserId { get; set; }
    }
}
