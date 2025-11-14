
namespace PrasTestJobDTO
{
    public class NewsDto
    {
        public Guid Id { get; init; }
        public string Headline { get; set; }
        public string? SubTitle { get; set; }
        public string Text { get; set; }
        public string? ImageData { get; set; }
        public string? ImageType { get; set; }
    }
}
