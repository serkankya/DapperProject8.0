using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.DTOs.VehicleDetailDtos
{
	public class UpdateVehicleDetailDto
	{
        public int VehicleId { get; set; }
        public string EngineType { get; set; }
        public string Transmission { get; set; }
        public string FuelType { get; set; }
        public decimal Mileage { get; set; }
        public Int16 NumberOfSeats { get; set; }
        public string Description { get; set; }
    }
}
