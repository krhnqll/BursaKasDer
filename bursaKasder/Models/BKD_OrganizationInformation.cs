using System.ComponentModel.DataAnnotations;

namespace bursaKasder.Models
{
    public class BKD_OrganizationInformation
    {

        [Key]
        public int OI_ID { get; set; }
        [Required]
        public string? OI_Name { get; set; }
        [Required]
        public string? OI_Logo { get; set; }
        [Required]
        public string? OI_StatuePhoto { get; set; }
        [Required]
        public string OI_Indexphoto { get; set; }
        [Required]
        public int? OI_Status { get; set; }

        public int? Counter { get; set; }

    }
}
