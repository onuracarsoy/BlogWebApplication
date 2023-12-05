using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.ValidationRules
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Kategori adı boş geçilemez!");
            RuleFor(x => x.CategoryName).MaximumLength(20).WithMessage("Lütfen fazla 20 karakter girişi yapın!");
            RuleFor(x => x.CategoryName).MinimumLength(2).WithMessage("Lütfen en az 2 karakter girişi yapın!");
            RuleFor(x => x.CategoryDescription).NotEmpty().WithMessage("Kategori adı boş geçilemez!");
        }
    }
}