using AionClass.Backend.DTOs;

namespace AionClass.Backend.Models.DTO
{
    public class MatriculaCursoDTO
    {
        public int Id { get; set; }
        public DateTime DataMatricula { get; set; }
        public CursoDto Curso { get; set; }
    }
}
