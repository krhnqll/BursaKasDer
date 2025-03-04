using System.ComponentModel.DataAnnotations;

namespace bursaKasder.Models
{
    public class BKD_Contact
    {
        [Key]
        public int con_ID { get; set; }

        [Required]
        public string con_Adress { get; set; }

        [Required]
        public string con_Phone { get; set; }
        
        public string con_PhoneSecond { get; set; }
        [Required]
        public string con_Email { get; set; }

        public string con_EmailSecond { get; set; }
        
        public string con_URLInstagram { get; set; }
        
        public string con_URLFacebook { get; set; }
        
        public string con_URLYoutube { get; set; }
        
        public string con_URLX { get; set; }

        [Required]
        public int con_Status { get; set; }
    }
}
