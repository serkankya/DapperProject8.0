using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.DTOs.BlogDtos
{
    public class InsertBlogDto
    {
        public int UserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
		public string? PreTitle { get; set; }
		public string? PreDescription { get; set; }
		public string? ImageUrl { get; set; }
    }
}
