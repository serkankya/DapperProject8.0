using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.DTOs.VehicleDtos
{
	public class ResultVehicleDto
	{
		public int VehicleId { get; set; }
		public int CategoryId { get; set; }
		public int ModelId { get; set; }
		public string? LicensePlate { get; set; }
		public string? Year { get; set; }
		public string? Color { get; set; }
		public decimal PricePerDay { get; set; }
		public DateTime InsertedDate { get; set; }
		public int InsertedBy { get; set; }
		public bool Status { get; set; }
	}
}
