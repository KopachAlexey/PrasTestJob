using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrasTestJobDTO
{
    public class NewsDto
    {
        public Guid Id { get; init; }
        public string Headline { get; set; }
        public string? SubTitle { get; set; }
        public string Text { get; set; }
        public byte[]? ImageData { get; set; }
        public string? ImageType { get; set; }
    }
}
