using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.DTOs.VehicleAmenitiesDtos
{
	public class UpdateVehicleAmenityDto
	{
		public int VehicleAmenityId { get; set; }
		public int VehicleId { get; set; }
		public int AmenityId { get; set; }
		public bool Status { get; set; }
	}
}
