using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.DTOs.BlogDtos
{
    public class ResultBlogDto
    {
        public int BlogId { get; set; }
        public int UserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? PreTitle { get; set; }
        public string? PreDescription { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime PostDate { get; set; }
        public bool Status { get; set; }
    }
}
