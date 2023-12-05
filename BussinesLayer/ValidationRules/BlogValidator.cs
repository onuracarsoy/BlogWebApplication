using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.ValidationRules
{
    public class BlogValidator : AbstractValidator<Blog>
    {
        public BlogValidator() 
        {
            RuleFor(x => x.BlogTitle).NotEmpty().WithMessage("Başlık boş geçilemez!");
            RuleFor(x => x.BlogTitle).MaximumLength(100).WithMessage("Lütfen fazla 100 karakter girişi yapın!");
            RuleFor(x => x.BlogTitle).MinimumLength(10).WithMessage("Lütfen en az 10 karakter girişi yapın!");
            RuleFor(x => x.BlogContent).NotEmpty().WithMessage("İçerik ksımını boş geçilemez!");
            RuleFor(x => x.BlogContent).MinimumLength(200).WithMessage("Lütfen fazla 200 karakter girişi yapın!");
            RuleFor(x => x.BlogImage).NotEmpty().WithMessage("Görsel kısmını boş geçilemez!");
            RuleFor(x => x.BlogThumbnailImage).NotEmpty().WithMessage(" Küçük görsel kısmını boş geçilemez!");
           
        }  
    }
}
