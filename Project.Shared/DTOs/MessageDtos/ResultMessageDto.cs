﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.DTOs.MessageDtos
{
    public class ResultMessageDto
    {
        public int MessageId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime SentDate { get; set; }
    }
}
