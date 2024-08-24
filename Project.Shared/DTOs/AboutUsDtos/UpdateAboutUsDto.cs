using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.DTOs.AboutUsDtos
{
	public class UpdateAboutUsDto
	{
        public int AboutUsId { get; set; }
        public string? Title { get; set; }
		public string? Description { get; set; }
		public string? MainImageUrl { get; set; }
	}
}
