using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.DTOs.ModelDtos
{
	public class ResultModelDto
	{
		public int ModelId { get; set; }
		public int BrandId { get; set; }
		public string? ModelName { get; set; }
		public bool Status { get; set; }
	}
}
