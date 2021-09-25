using Application;
using Application.Commands.UserAddressCommands;
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

namespace Implementation.Commands.UserAddressCommands
{
    public class EfCreateUserAddressCommand : EfCommand, ICreateUserAddressCommand
    {
        private readonly UserAddressValidator _validator;
        private readonly IMapper _mapper;
        private readonly IApplicationActor _actor;
        public EfCreateUserAddressCommand(EcomShopContext context, UserAddressValidator validator, IMapper mapper, IApplicationActor actor) : base(context)
        {
            _validator = validator;
            _mapper = mapper;
            _actor = actor;
        }

        public int Id => 20;

        public string Name => "Ef Create Address For A User";

        public void Execute(UserAddressDto request)
        {
            _validator.ValidateAndThrow(request); 
            var address = _mapper.Map<UserAddress>(request);
            address.UserId = _actor.Id;
            Context.Add(address);
            Context.SaveChanges();
        }
    }
}
