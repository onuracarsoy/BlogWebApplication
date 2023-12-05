using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.ValidationRules
{
	public class WriterValidator : AbstractValidator<Writer>
	{
		public WriterValidator() {
			RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar adı soyadı boş geçilemez!");

			RuleFor(x => x.WriterPassword)
		   .NotEmpty().WithMessage("Şifre Boş geçilemez!")
		   .Matches("[A-Z]").WithMessage("En az bir büyük harf içermelidir!")
		   .Matches("[a-z]").WithMessage("En az bir küçük harf içermelidir!")
		   .Matches("[0-9]").WithMessage("En az bir sayı içermelidir!")
		   .Length(8, 15).WithMessage("Şifre 8-15 karakter uzunluğunda olmalıdır!");

			RuleFor(x => x.WriterMail).NotEmpty().WithMessage("Mail adresi boş geçilemez!");
			
			RuleFor(x => x.WriterName).MinimumLength(2).WithMessage("Lütfen en az 2 karakter girişi yapın!");
			RuleFor(x => x.WriterName).MaximumLength(50).WithMessage("Lütfen fazla 50 karakter girişi yapın!");
			
		}	
	}
}
