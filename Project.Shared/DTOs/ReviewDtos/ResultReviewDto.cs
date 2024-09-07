using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.DTOs.Reviews
{
	public class ResultReviewDto
	{
        public int ReviewId { get; set; }
        public int VehicleId { get; set; }
        public string? FullName { get; set; }
        public string? Comment { get; set; }
        public Int16 Stars { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime ReviewDate { get; set; }
        public bool Status { get; set; }
    }
}
