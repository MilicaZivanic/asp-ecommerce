using Application.Data_Transfer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class UserAddressValidator : AbstractValidator<UserAddressDto>
    {
        public UserAddressValidator()
        {
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is mandatory.");
            RuleFor(x => x.City).NotEmpty().WithMessage("City is mandatory");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country is mandatory");
            RuleFor(x => x.PostalCode).NotEmpty().WithMessage("Postal Code is mandatory");
            RuleFor(x => x.Telephone).NotEmpty().WithMessage("Telephone is mandatory");
        }
    }
}
