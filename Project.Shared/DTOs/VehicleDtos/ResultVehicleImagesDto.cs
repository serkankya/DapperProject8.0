﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.DTOs.VehicleDtos
{
    public class ResultVehicleImagesDto
    {
        public int VehicleImageId { get; set; }
        public int VehicleId { get; set; }
        public string? ImageUrl { get; set; }
    }
}
