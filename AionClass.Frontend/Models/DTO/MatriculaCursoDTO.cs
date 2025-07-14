namespace AionClass.Frontend.Models.DTO
{
    public class MatriculaCursoDTO
    {
        public int Id { get; set; }
        public DateTime DataMatricula { get; set; }
        public CursoDTO Curso { get; set; }
    }
}
