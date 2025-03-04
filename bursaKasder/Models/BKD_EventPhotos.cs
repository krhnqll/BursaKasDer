using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bursaKasder.Models
{
    public class BKD_EventPhotos
    {
        [Key]
        public int evP_ID { get; set; }

        [Required]
        public string? evP_Photo { get; set; }

        [Required]
        public int? evP_EventId { get; set; }

        [Required]
        public int evP_Status { get; set; }

        public BKD_Events? Event { get; set; }
    }
}
