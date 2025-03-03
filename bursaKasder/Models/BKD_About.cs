using System.ComponentModel.DataAnnotations;

namespace bursaKasder.Models
{
    public class BKD_About
    {
        [Key]
        public int ab_ID { get; set; }

        [Required]
        public string ab_History {  get; set; }

        [Required]
        public string ab_HistoryPhoto { get; set; }

        [Required]
        public string ab_MisVis { get; set; }

        [Required]
        public string ab_MisVisPhoto { get; set ; }

        [Required]
        public int ab_Status { get; set; }
    }
}
