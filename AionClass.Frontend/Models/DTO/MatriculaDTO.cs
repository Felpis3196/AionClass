namespace AionClass.Frontend.Models.DTO
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
