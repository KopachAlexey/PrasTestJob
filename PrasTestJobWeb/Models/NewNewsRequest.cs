namespace PrasTestJobWeb.Models
{
    public class NewNewsRequest
    {
        public string Headline { get; init; }
        public string? SubTitle { get; init; }
        public string Text { get; init; }
        public IFormFile ImageFormFile { get; init; }
    }
}
