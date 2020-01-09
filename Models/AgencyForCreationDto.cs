using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AtlantisPortals.API.Models
{
    public class AgencyForCreationDto
    {

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Ministry { get; set; }

        [MaxLength(150)]
        public string EmailAddress { get; set; }

        [MaxLength(30)]
        public string ContactNumber { get; set; }

        [Required]
        [MaxLength(10)]
        public string Prefix { get; set; }

        [Required]
        public int DigitLength { get; set; }
        public int MinUnitThreshold { get; set; }
        [Required]
        public int ReceiptTypeId { get; set; }
    }
}
