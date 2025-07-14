namespace AionClass.Backend.DTOs
{
    public class MatriculaDto
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public string CursoTitulo { get; set; }
        public string ThumbnailUrl { get; set; }
        public DateTime DataMatricula { get; set; }
    }
}
