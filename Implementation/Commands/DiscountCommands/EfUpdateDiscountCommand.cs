using Application.Commands.DiscountCommands;
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

namespace Implementation.Commands.DiscountCommands
{
    public class EfUpdateDiscountCommand : EfCommand, IUpdateDiscountCommand
    {
        private readonly DiscountValidator _validator;
        private readonly IMapper _mapper;
        public EfUpdateDiscountCommand(EcomShopContext context, DiscountValidator validator, IMapper mapper) : base(context)
        {
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 29;

        public string Name => "Ef Update Discount";

        public void Execute(DiscountDto request)
        {
            _validator.ValidateAndThrow(request);

            var discount = Context.Categories.Find(request.Id);
            if (discount == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Discount));
            }

            _mapper.Map(request, discount);
            Context.SaveChanges();
        }
    }
}
