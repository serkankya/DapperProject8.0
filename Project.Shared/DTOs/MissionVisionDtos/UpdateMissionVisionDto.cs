using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.DTOs.MissionVisionDtos
{
    public class UpdateMissionVisionDto
    {
        public int Id { get; set; }
        public string? MissionTitle { get; set; }
        public string? MissionText { get; set; }
        public string? VisionTitle { get; set; }
        public string? VisionText { get; set; }
    }
}
