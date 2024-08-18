using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.DTOs.VehicleDtos
{
	public class InsertVehicleDto
	{
		public int CategoryId { get; set; }
		public int ModelId { get; set; }
		public string LicensePlate { get; set; }
		public string Year { get; set; }
		public string Color { get; set; }
		public decimal PricePerDay { get; set; }
	}
}
