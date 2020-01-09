using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtlantisPortals.API.Models
{
    public class UserLoginSuccessfulDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string AgencyId { get; set; }
    }
}
