using System.ComponentModel.DataAnnotations;

namespace bursaKasder.Models
{
    public class BKD_ContactUs
    {
        [Key]
        public int conU_ID { get; set; }

        [Required]
        public string conU_Name { get; set; }
        [Required]
        public string conU_Surname { get; set; }
        [Required]
        public string conU_Email { get; set; }
        [Required]
        public string conU_Title { get; set; }
        [Required]
        public string conU_Message { get; set; }
        [Required]
        public DateTime conU_DateMessage { get; set; }
        [Required]
        public int conU_Status { get; set; }
    }
}
