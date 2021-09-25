using Application;
using Application.Commands.UserPaymentCommands;
using Application.Data_Transfer;
using Application.Exceptions;
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
    public class EfUpdateUserPaymentCommand : EfCommand, IUpdateUserPaymentCommand
    {
        private readonly UserPaymentValidator _validator;
        private readonly IMapper _mapper;
        private readonly IApplicationActor _actor;
        public EfUpdateUserPaymentCommand(EcomShopContext context, UserPaymentValidator validator, IMapper mapper, IApplicationActor actor) : base(context)
        {
            _validator = validator;
            _mapper = mapper;
            _actor = actor;
        }

        public int Id => 25;

        public string Name => "Ef Update User Payment Command";

        public void Execute(UserPaymentDto request)
        {
            _validator.ValidateAndThrow(request);

            var payment = Context.UserPayments.Where(x => x.UserId == _actor.Id).FirstOrDefault(x => x.Id == request.Id);
            if (payment == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(UserPayment));
            }

            _mapper.Map(request, payment);
            Context.SaveChanges();
        }
    }
}
