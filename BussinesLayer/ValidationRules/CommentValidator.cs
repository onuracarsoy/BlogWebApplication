using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.ValidationRules
{
    public class CommentValidator : AbstractValidator<Comment>
    {
        public CommentValidator()
        {
            RuleFor(x => x.CommentTitle).NotEmpty().WithMessage("Konu boş geçilemez!");
            RuleFor(x => x.CommentContent).NotEmpty().WithMessage("Yorum boş geçilemez!");
        }
    }
}
