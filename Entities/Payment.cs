using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtlantisPortals.API.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal AmountBalance { get; set; }
        [Required]
        public int Unit { get; set; }
        [Required]
        public int MinUnitThreshold { get; set; }
        [Required]
        [MaxLength(100)]
        public string ReceiptType { get; set; }
        [Required]
        public int UnitPerReceiptType { get; set; }
        [Required]
        public int CurrentReceiptNumber { get; set; }

        [ForeignKey("AgencyId")]
        public Agency Agency { get; set; }
        public int AgencyId { get; set; }

        public ICollection<PaymentDetail> PaymentDetails { get; set; } = new List<PaymentDetail>();

    }
}
