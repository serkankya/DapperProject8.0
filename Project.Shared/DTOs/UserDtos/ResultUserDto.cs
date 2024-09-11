using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.DTOs.UserDtos
{
    public class ResultUserDto
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AboutUser { get; set; }
        public string? Email { get; set; }
        public string? ImageUrl { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
    }
}
