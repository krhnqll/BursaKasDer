using System.ComponentModel.DataAnnotations;

namespace bursaKasder.Models.EntityModels
{
    public class addOIimage
    {
        [Key]
        public int OI_ID { get; set; }
        [Required]
        public string? OI_Name { get; set; }
        [Required]
        public IFormFile OI_Logo { get; set; }
        public string? OI_LogoPath { get; set; }
        [Required]
        public IFormFile OI_StatuePhoto { get; set; }
        public string? OI_StatuePhotoPath { get; set; }
        [Required]
        public IFormFile OI_Indexphoto { get; set; }
        public string? OI_IndexphotoPath { get; set; }
        [Required]
        public int? OI_Status { get; set; }
    }
}
