using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtlantisPortals.API.Models
{
    public class AgencyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ministry { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
        public string Prefix { get; set; }
        public int DigitLength { get; set; }
    }
}
