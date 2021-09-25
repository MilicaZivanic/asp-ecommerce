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
    public class CreateOrderValidator : AbstractValidator<OrderDto>
    {
        public CreateOrderValidator(EcomShopContext context)
        {

            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required.");
            RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required.");
            RuleFor(x => x.PostalCode).NotEmpty().WithMessage("Postal code is required.");
            RuleFor(x => x.AccountNumber).NotEmpty().WithMessage("Account number is required.");

            RuleFor(x => x.OrderItems).NotEmpty().WithMessage("There must be at least one order item").DependentRules(() =>
            {
                RuleFor(x => x.OrderItems).Must(orderItems =>
                {
                    var distinctProductIds = orderItems.Select(x => x.ProductId).Distinct();

                    return distinctProductIds.Count() == orderItems.Count();
                }).WithMessage("There are duplicate order lines.").DependentRules(() =>
                {
                    RuleForEach(x => x.OrderItems).SetValidator(new CreateOrderItemValidator(context));
                });
            });
    }
    }
}
