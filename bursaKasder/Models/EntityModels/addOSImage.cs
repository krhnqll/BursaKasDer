using System.ComponentModel.DataAnnotations;

namespace bursaKasder.Models.EntityModels
{
    public class addOSImage
    {
        [Key]
        public int OS_ID { get; set; }

        [Required]
        public string? OS_Name { get; set; }
        public string? OS_Surname { get; set; }
        public string? OS_Degree { get; set; }
        public IFormFile OS_Photo { get; set; }
        public string? OS_PhotoPath { get; set; }
        public string? OS_Comment { get; set; }
        public int OS_Status { get; set; }
    }
}
