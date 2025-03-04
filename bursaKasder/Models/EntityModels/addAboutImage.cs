using Microsoft.Extensions.FileProviders;
using System.ComponentModel.DataAnnotations;

namespace bursaKasder.Models.EntityModels
{
    public class addAboutImage
    {
        [Key]
        public int ab_ID { get; set; }

        [Required]
        public string ab_History { get; set; }

        [Required]
        public IFormFile ab_HistoryPhoto { get; set; }

        [Required]
        public string ab_MisVis { get; set; }

        [Required]
        public IFormFile ab_MisVisPhoto { get; set; }

        [Required]
        public int ab_Status { get; set; }
    }
}
