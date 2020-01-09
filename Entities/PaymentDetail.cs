using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtlantisPortals.API.Entities
{
    public class PaymentDetail : AuditableEntity
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalAmount { get; set; }
        [Required]
        public int Unit { get; set; }
        [Required]
        [MaxLength(60)]
        public string PaymentMethod { get; set; }
        [MaxLength(150)]
        public string PaymentGatewayRef { get; set; }
        public bool PaymentVerified { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal CAGDAmount { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal PDQAmount { get; set; }

        public int AgencyId { get; set; }
        [MaxLength(200)]
        public string AgencyName { get; set; }

        [ForeignKey("PaymentId")]
        public Payment Payment { get; set; }
        public int PaymentId { get; set; }
    }
}
