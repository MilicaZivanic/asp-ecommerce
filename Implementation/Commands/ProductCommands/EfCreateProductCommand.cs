using Application.Commands;
using Application.Commands.ProductCommands;
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

namespace Implementation.Commands.ProductCommands
{
    public class EfCreateProductCommand : EfCommand, ICreateProductCommand
    {
        private readonly CreateProductValidator _validator;
        private readonly IMapper _mapper;
        public EfCreateProductCommand(EcomShopContext context, CreateProductValidator validator, IMapper mapper) : base(context)
        {
            _validator = validator;
            _mapper = mapper;
        }
        public int Id => 10;

        public string Name => "Ef Create Product";

        public void Execute(ProductDto request)
        {
            _validator.ValidateAndThrow(request); 
            var product = _mapper.Map<Product>(request);
            Context.Add(product);
            Context.SaveChanges();
        }
    }
}
