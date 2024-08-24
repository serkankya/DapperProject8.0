using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.DTOs.VehicleDtos
{
    public class ResultVehicleDto
    {
        //Vehicles
        public int VehicleId { get; set; }
        public int CategoryId { get; set; }
        public int ModelId { get; set; }
        public string? LicensePlate { get; set; }
        public string? Year { get; set; }
        public string? Color { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsSelectedCar { get; set; }
        public DateTime InsertedDate { get; set; }
        public int InsertedBy { get; set; }
        public bool Status { get; set; }


        //VehicleDetails
        public string? EngineType { get; set; }
        public string? Transmission { get; set; }
        public string? FuelType { get; set; }
        public string? Description { get; set; }
        public int NumberOfSeats { get; set; }
        public decimal MileAge { get; set; }


        //Independent
        public string? CategoryName { get; set; }
        public string? ModelName { get; set; }
        public string? BrandName { get; set; }
    }
}
