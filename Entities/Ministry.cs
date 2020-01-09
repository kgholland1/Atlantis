using System.ComponentModel.DataAnnotations;

namespace AtlantisPortals.API.Entities
{
    public class Ministry
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

    }
}
