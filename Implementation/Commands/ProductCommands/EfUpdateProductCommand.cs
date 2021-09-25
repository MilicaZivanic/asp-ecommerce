using Application.Commands;
using Application.Commands.ProductCommands;
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

namespace Implementation.Commands.ProductCommands
{
    public class EfUpdateProductCommand : EfCommand,IUpdateProductCommand
    {
        private readonly UpdateProductValidator _validator;
        private readonly IMapper _mapper;
        public EfUpdateProductCommand(EcomShopContext context, UpdateProductValidator validator, IMapper mapper) : base(context)
        {
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 11;

        public string Name => "Ef Update Product";

        public void Execute(ProductDto request)
        {
            _validator.ValidateAndThrow(request);

            var product = Context.Products.Find(request.Id);
            if (product == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Product));
            }

            _mapper.Map(request, product);
            Context.SaveChanges();
        }
    }
}
