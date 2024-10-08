﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.DTOs.CommentDtos
{
	public class UpdateCommentDto
	{
		public int CommentId { get; set; }
		public string? Comment { get; set; }
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public string? ImageUrl { get; set; }
		public DateTime CommentDate { get; set; }
	}
}
