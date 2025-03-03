using System.ComponentModel.DataAnnotations;

namespace bursaKasder.Models
{
    public class BKD_NewsFromKast
    {

        [Key]
        public int newsK_Id { get; set; }

        [Required]
        public string newsK_URL { get; set; }

        [Required]
        public DateTime newsK_AddedDate { get; set; }

        [Required]
        public int newsK_Status { get; set; }
    }
}
