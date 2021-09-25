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
    public class CreateProductValidator : AbstractValidator<ProductDto>
    {
        public CreateProductValidator(EcomShopContext context)
        {
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category is required.").DependentRules(() =>
            {
                RuleFor(x => x.CategoryId).Must(x =>
                {
                    return context.Categories.Any(y => y.Id == x);
                }).WithMessage("Provided category doesn't exist.");
            });

            RuleFor(x => x.Price).Must(x => x >= 0.1m).WithMessage("Min value for price is $0.1.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.DiscountId).Must(x =>
            {
                return context.Discounts.Any(y => y.Id == x);
            }).WithMessage("Provided discount doesn't exist.");

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.").DependentRules(() =>
            {
                RuleFor(x => x.Name).Must(x =>
                {
                    return !context.Products.Any(y => y.Name == x);
                }).WithMessage(y => $"Provided product name: {y.Name} already exists in database.");
            });
        }
    }
}
