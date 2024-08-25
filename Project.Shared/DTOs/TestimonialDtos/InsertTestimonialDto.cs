using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.DTOs.TestimonialDtos
{
	public class InsertTestimonialDto
	{
		public string? ImageUrl { get; set; }
		public string? FullName { get; set; }
		public string? Description { get; set; }
		public string? JobTitle { get; set; }
		public bool Status { get; set; }
	}
}
