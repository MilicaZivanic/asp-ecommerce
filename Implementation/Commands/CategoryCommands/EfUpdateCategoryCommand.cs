using Application.Commands;
using Application.Commands.CategoryCommands;
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

namespace Implementation.Commands.CategoryCommands
{
    public class EfUpdateCategoryCommand : EfCommand, IUpdateCategoryCommand
    {
        private readonly UpdateCategoryValidator _validator;
        private readonly IMapper _mapper;

        public EfUpdateCategoryCommand(EcomShopContext context, UpdateCategoryValidator validator, IMapper mapper) : base(context)
        {
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 7;

        public string Name => "Ef Update Category";

        public void Execute(CategoryDto request)
        {
            _validator.ValidateAndThrow(request);

            var category = Context.Categories.Find(request.Id);
            if (category == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Category));
            }

            _mapper.Map(request, category);
            Context.SaveChanges();

        }
    }
}
