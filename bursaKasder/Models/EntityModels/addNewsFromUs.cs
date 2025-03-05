using System.ComponentModel.DataAnnotations;

namespace bursaKasder.Models.EntityModels
{
    public class addNewsFromUs
    {
        [Key]
        public int newsU_ID { get; set; }
        [Required]
        public string? newsU_Title { get; set; }
        [Required]
        public string newsU_Content { get; set; }
        [Required]
        public IFormFile newsU_Photo { get; set; }
        [Required]
        public DateTime newsU_Date { get; set; }
        [Required]
        public int? newsU_Status { get; set; }
    }
}
