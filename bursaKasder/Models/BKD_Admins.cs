using System.ComponentModel.DataAnnotations;

namespace bursaKasder.Models
{
    public class BKD_Admins
    {
        [Key]
        public int adm_ID { get; set; }
        [Required]
        public string adm_Name { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz.")]
        public string adm_Username { get; set; }
        [Required]
        public string adm_Surname { get; set; }

        [Required(ErrorMessage = "Şifre boş bırakılamaz.")]
        public string adm_Password { get; set; }
        [Required]
        public int adm_Status { get; set; }

    }
}
