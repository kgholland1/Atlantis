using System.ComponentModel.DataAnnotations;

namespace AtlantisPortals.API.Models
{
    public class AgencyForUpdateDto
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
    }
}
