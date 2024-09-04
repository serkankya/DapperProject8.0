using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.DTOs.VehicleAmenitiesDtos
{
	public class ResultAmenityDto
	{
        public int AmenityId { get; set; }
        public string? Title { get; set; }
        public bool Status { get; set; }
    }
}
