using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.DTOs.CommentDtos
{
	public class InsertCommentDto
	{
		public int BlogId { get; set; }
		public int UserId { get; set; }
		public string? Comment { get; set; }
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public string? ImageUrl { get; set; }
	}
}
