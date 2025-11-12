using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrasTestJobData.Entities
{
    public class News
    {
        private Guid _id;

        public Guid Id => _id;
        public string Headline { get; set; }
        public string? SubTitle { get; set; }
        public string Text { get; set; }
        public byte[]? ImageData { get; set; }
        public string? ImageType { get; set; }

    }
}
