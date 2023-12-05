using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace MyAspNetCoreApp.Web.Models
{
    public class UserSignUpViewModel
    {
        [Display(Name = "Ad Soyad")]
        [Required(ErrorMessage = "Ad soyad boş geçilemez!")]

        public string NameSurname { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre boş geçilemez!")]

        public string Password { get; set; }

        [Display(Name = " Tekrar Şifre")]
		[Required(ErrorMessage = "Tekrar Şifre boş geçilemez!")]
		[Compare("Password", ErrorMessage ="Şifreler uyuşmuyor!")]

        public string ConfirmPassword { get; set; }

        [Display(Name = "Mail Adresi")]
        [Required(ErrorMessage = "Mail boş geçilemez!")]

        public string Mail { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez!")]

        public string UserName { get; set; }

    }
}
