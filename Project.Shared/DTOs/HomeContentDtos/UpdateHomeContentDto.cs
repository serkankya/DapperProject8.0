using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.DTOs.HomeContentDtos
{
    public class UpdateHomeContentDto
    {
        public int HomeContentId { get; set; }
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public string? VideoTitle { get; set; }
        public string? VideoUrl { get; set; }
        public string? BackgroundImage { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
