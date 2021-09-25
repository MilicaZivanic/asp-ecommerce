using Application;
using Application.Commands.UserPaymentCommands;
using Application.Data_Transfer;
using AutoMapper;
using Domain;
using EfDataAccess;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.UserPaymentCommands
{
    public class EfCreateUserPaymentCommand : EfCommand, ICreateUserPaymentCommand
    {
        private readonly UserPaymentValidator _validator;
        private readonly IMapper _mapper;
        private readonly IApplicationActor _actor;
        public EfCreateUserPaymentCommand(EcomShopContext context, UserPaymentValidator validator, IMapper mapper, IApplicationActor actor) : base(context)
        {
            _validator = validator;
            _mapper = mapper;
            _actor = actor;
        }

        public int Id => 23;

        public string Name => "Ef Create Payment For A User";

        public void Execute(UserPaymentDto request)
        {
            _validator.ValidateAndThrow(request); 
            var payment = _mapper.Map<UserPayment>(request);
            payment.UserId = _actor.Id;
            Context.Add(payment);
            Context.SaveChanges();
        }
    }
}
