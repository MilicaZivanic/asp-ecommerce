using Application.Data_Transfer;
using EfDataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CategoryDto>
    {
        public CreateCategoryValidator(EcomShopContext context)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is mandatory").DependentRules(() =>
                {
                    RuleFor(x => x.Name).Must(name => !context.Categories.Any(g => g.Name == name)).WithMessage("Group name must be unique");
                });
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description in mandatory");

        }
    }
}
