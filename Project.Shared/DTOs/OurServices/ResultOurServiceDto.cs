using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.DTOs.OurServices
{
    public class ResultOurServiceDto
    {
        public int ServiceId { get; set; }
        public string? Title { get; set; }
        public string? Icon { get; set; }
        public string? Description { get; set; }
        public bool IsSelected { get; set; }    
        public bool Status { get; set; }
    }
}
