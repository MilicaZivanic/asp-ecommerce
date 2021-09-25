using Application.Commands;
using Application.Commands.UserCommands;
using Application.Data_Transfer;
using Application.Email;
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
    public class EfRegisterUserCommand : EfCommand,IRegisterUserCommand
    {
        private readonly RegisterUserValidator _validator;
        private readonly IEmailSender _sender;
        private readonly IMapper _mapper;

        public EfRegisterUserCommand(EcomShopContext context, IEmailSender sender, RegisterUserValidator validator, IMapper mapper) : base(context)
        {
            _sender = sender;
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 2;

        public string Name => "Ef Register User";

        public void Execute(UserDto request)
        {
            _validator.ValidateAndThrow(request);
            var user = _mapper.Map<User>(request);
            Context.Add(user);
            Context.SaveChanges();

            _sender.Send(new SendEmailDto
            {
                Content = "<h1>Successfull registration!</h1>",
                SendTo = request.Email,
                Subject = "Registration"
            });
        }
    }
}
