using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AtlantisPortals.API.Entities
{
    public class ReceiptType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Type { get; set; }

        [Required]
        public int UnitPerType { get; set; }
    }
}
