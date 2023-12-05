using System.ComponentModel.DataAnnotations;

namespace MyAspNetCoreApp.Web.Models
{
	public class UserSignInViewModel
	{
        [Required(ErrorMessage ="Kullanıcı adı giriniz!")]
        public string username { get; set; }
		[Required(ErrorMessage = "Şifre giriniz!")]
		public string password { get; set; }
    }
}
