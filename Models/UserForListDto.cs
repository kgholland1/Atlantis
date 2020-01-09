using System;
using System.Collections.Generic;

namespace AtlantisPortals.API.Models
{
    public class UserForListDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime LastActive { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}
