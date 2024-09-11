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
        public string? FirstTitle { get; set; }
        public string? FirstDescription { get; set; }
        public string? SecondTitle { get; set; }
        public string? SecondDescription { get; set; }
        public string? PreTitle { get; set; }
        public string? PreDescription { get; set; }
        public string? ImageUrl { get; set; }
        public int CommentCount { get; set; }
        public DateTime PostDate { get; set; }
        public bool Status { get; set; }
    }
}
