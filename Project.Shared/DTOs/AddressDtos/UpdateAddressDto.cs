﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.DTOs.AddressDtos
{
    public class UpdateAddressDto
    {
        public int AddressId { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? LocationUrl { get; set; }
    }
}
