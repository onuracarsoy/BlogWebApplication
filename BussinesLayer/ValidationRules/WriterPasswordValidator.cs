using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.ValidationRules
{
    public class WriterPasswordValidator : AbstractValidator<Writer>
    {
        public WriterPasswordValidator()
        {
         

            RuleFor(x => x.WriterPassword)
           .NotEmpty().WithMessage("Şifre Boş geçilemez!")
           .Matches("[A-Z]").WithMessage("En az bir büyük harf içermelidir!")
           .Matches("[a-z]").WithMessage("En az bir küçük harf içermelidir!")
           .Matches("[0-9]").WithMessage("En az bir sayı içermelidir!")
           .Length(8, 15).WithMessage("Şifre 8-15 karakter uzunluğunda olmalıdır!");



        }
    }
}
