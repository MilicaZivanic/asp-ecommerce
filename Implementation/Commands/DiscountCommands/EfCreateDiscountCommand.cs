using Application.Commands.DiscountCommands;
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

namespace Implementation.Commands.DiscountCommands
{
    public class EfCreateDiscountCommand : EfCommand, ICreateDiscountCommand
    {
        private readonly DiscountValidator _validator;
        private readonly IMapper _mapper;
        public EfCreateDiscountCommand(EcomShopContext context, DiscountValidator validator, IMapper mapper) : base(context)
        {
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 28;

        public string Name => "Ef Create Discount";

        public void Execute(DiscountDto request)
        {
            _validator.ValidateAndThrow(request);
            var discount = _mapper.Map<Discount>(request);
            Context.Add(discount);
            Context.SaveChanges();
        }
    }
}
