using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtlantisPortals.API.Entities
{
    public class Receipt
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string ReceiptNumber { get; set; }
        [MaxLength(30)]
        public string ParentReceiptNumber { get; set; }
        [Required]
        public DateTime ReceiptDate { get; set; }
        [MaxLength(100)]
        public string RecipentName { get; set; }
        [MaxLength(100)]
        public string RecipentEmail { get; set; }
        [MaxLength(25)]
        public string RecipentPhone { get; set; }
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }
        [Required]
        [MaxLength(100)]
        public string Status { get; set; }
        [MaxLength(100)]
        public string ReceiptType { get; set; }
        [MaxLength(150)]
        public string ReceiptUrl { get; set; }
        [Required]
        public Guid SecurityCode { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
