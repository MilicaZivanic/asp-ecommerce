using Application;
using Application.Commands.UserAddressCommands;
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

namespace Implementation.Commands.UserAddressCommands
{
    public class EfUpdateUserAddressCommand : EfCommand, IUpdateUserAddressCommand
    {
        private readonly UserAddressValidator _validator;
        private readonly IMapper _mapper;
        private readonly IApplicationActor _actor;

        public EfUpdateUserAddressCommand(EcomShopContext context, UserAddressValidator validator, IMapper mapper, IApplicationActor actor) : base(context)
        {
            _validator = validator;
            _mapper = mapper;
            _actor = actor;
        }

        public int Id => 21;

        public string Name => "Ef Update Address For A User";

        public void Execute(UserAddressDto request)
        {
            _validator.ValidateAndThrow(request);

            var address = Context.UserAddresses.Where(x => x.UserId == _actor.Id).FirstOrDefault(x => x.Id == request.Id);
            if (address == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(UserAddress));
            }

            _mapper.Map(request, address);
            Context.SaveChanges();
        }
    }
}
