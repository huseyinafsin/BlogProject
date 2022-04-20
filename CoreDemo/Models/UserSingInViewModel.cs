
using System.ComponentModel.DataAnnotations;

namespace CoreDemo.Models
{
    public class UserSingInViewModel
    {
        [Required(ErrorMessage = "Lütfen kullnıcı adınızı girin")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi girin")]

        public string Password { get; set; }
    }
}
