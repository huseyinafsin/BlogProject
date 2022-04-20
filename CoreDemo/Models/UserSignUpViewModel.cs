using System.ComponentModel.DataAnnotations;

namespace CoreDemo.Models
{
    public class UserSignUpViewModel
    {
        [Required(ErrorMessage="Lütfen Ad Soyad Giriniz")]
        [Display(Name = "Ad Soyad")]
        public string NameSurname { get; set; }  


        [Required(ErrorMessage="Şifre giriniz")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }   

        [Compare("Password",ErrorMessage = "Şifreler uyuşmuyor")]
        [Display(Name = "Şifre Tekrar")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Email giriniz")]
        [Display(Name = "Mail Adres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Kulanıcı Adı giriniz")]
        [Display(Name = "Kulanıcı Adı")]
        public string Username { get; set; }
    }
}
