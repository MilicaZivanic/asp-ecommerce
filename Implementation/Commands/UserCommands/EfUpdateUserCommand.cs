using Application.Commands.UserCommands;
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

namespace Implementation.Commands.UserCommands
{
    public class EfUpdateUserCommand : EfCommand, IUpdateUserCommand
    {
        private readonly UpdateUserValidator _validator;
        private readonly IMapper _mapper;
        public EfUpdateUserCommand(EcomShopContext context, UpdateUserValidator validator, IMapper mapper) : base(context)
        {
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 15;

        public string Name => "Ef Update User ";

        public void Execute(UserDto request)
        {
            _validator.ValidateAndThrow(request);

            var user = Context.Users.Find(request.Id);
            if (user == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(User));
            }

            _mapper.Map(request, user);
            Context.SaveChanges();
        }
    }
}
