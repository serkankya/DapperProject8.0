using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.DTOs.AwardDtos
{
    public class InsertAwardDto
    {
        public string? AwardName { get; set; }
        public string? AwardDescription { get; set; }
        public DateTime AwardDate { get; set; }
        public string? AwardOrganization { get; set; }
        public string? AwardPhotoUrl { get; set; }
    }
}
