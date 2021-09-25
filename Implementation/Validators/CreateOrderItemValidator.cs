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
    public class CreateOrderItemValidator : AbstractValidator<OrderItemDto>
    {
        public CreateOrderItemValidator(EcomShopContext context)
        {
            RuleFor(x => x.Quantity).Must(x => x > 0).WithMessage("Quantity must be greather than 0.");
            RuleFor(x => x.ProductId).Must(x => context.Products.Any(p => p.Id == x)).WithMessage("Product doesn't exist.");
    }
    }
}
