using Application.Data_Transfer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class UserPaymentValidator : AbstractValidator<UserPaymentDto>
    {
        public UserPaymentValidator()
        {
            RuleFor(x => x.AccountNumber).NotEmpty().WithMessage("Account number is mandatory.");
            RuleFor(x => x.Expiry).NotEmpty().WithMessage("Expiry date is mandatory");
            RuleFor(x => x.PaymentType).NotEmpty().WithMessage("Payment type is mandatory");
            RuleFor(x => x.Provider).NotEmpty().WithMessage("Provider is mandatory");
        }
    }
}
