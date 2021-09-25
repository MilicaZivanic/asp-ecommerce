using Application.Data_Transfer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class DiscountValidator : AbstractValidator<DiscountDto>
    {
        public DiscountValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is mandatory.");
            RuleFor(x => x.DiscountPercent).NotEmpty().WithMessage("Discount percent is mandatory");
        }
    }
}
