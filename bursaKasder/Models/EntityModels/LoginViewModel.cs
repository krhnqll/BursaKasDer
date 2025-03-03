using System.ComponentModel.DataAnnotations;

namespace bursaKasder.Models.EntityModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz.")]
        public string adm_Username { get; set; }

        [Required(ErrorMessage = "Şifre boş bırakılamaz.")]
        [DataType(DataType.Password)]
        public string adm_Password { get; set; }
    }

}
