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
    public class UpdateUserValidator : AbstractValidator<UserDto>
    {
        public UpdateUserValidator(EcomShopContext context)
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is mandatory.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is mandatory");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is mandatory").DependentRules(() =>
            {
                RuleFor(x => x.Password).MaximumLength(10).WithMessage("Password is mandatory and is has to be maximum of 10 characters");
            });

            RuleFor(x => x.Email).EmailAddress().WithMessage("Email needs to be in form of an email(example@mail.com)").DependentRules(() =>
            {
                RuleFor(x => x.Email).Must((dto, email) => !context.Users.Any(user => user.Email == email && user.Id != dto.Id)).WithMessage("Username is already taken.");
            });
        }
    }
}
