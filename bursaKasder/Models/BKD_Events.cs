using System.ComponentModel.DataAnnotations;

namespace bursaKasder.Models
{
    public class BKD_Events
    {

        [Key]
        public int ev_ID { get; set; }

        [Required]
        public string? ev_Title { get; set; }
        [Required]
        public string? ev_Content { get; set; }

        [Required]
        public string? ev_MainPhoto { get; set; }
        [Required]
        public DateTime ev_Date { get; set; }

        [Required]
        public int? ev_Status { get; set; }

        public List<BKD_EventPhotos> EventPhotos { get; set; } = new List<BKD_EventPhotos>();

    }
}
