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
    public class UpdateCategoryValidator : AbstractValidator<CategoryDto>
    {
        public UpdateCategoryValidator(EcomShopContext context)
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is mandatory").DependentRules(() =>
            {
                RuleFor(x => x.Id).Must(x => context.Categories.Any(c => c.Id == x)).WithMessage(x => $"Id ${x.Id} doesn't exist in a database.").DependentRules(() =>
                {
                    RuleFor(x => x.Name).NotEmpty().WithMessage("Name is mandatory").DependentRules(() =>
                    {
                        RuleFor(x => x.Name).Must((dto, name) => !context.Categories.Any(c => c.Name == name && c.Id != dto.Id)).WithMessage(c => $"Category with the name of {c.Name} already exists in database.");
                    });
                    RuleFor(x => x.Description).NotEmpty().WithMessage("Description in mandatory");
                });
            });


        }
    }
}
