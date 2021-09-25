using Application.Commands.CategoryCommands;
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

namespace Implementation.Commands.CategoryCommands
{
    public class EfCreateCategoryCommand : EfCommand,ICreateCategoryCommand
    {
        private readonly CreateCategoryValidator _validator;
        private readonly IMapper _mapper;
        public EfCreateCategoryCommand(EcomShopContext context, CreateCategoryValidator validator, IMapper mapper) : base(context)
        {
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 5;

        public string Name => "Ef Create Category";

        public void Execute(CategoryDto request)
        {
            _validator.ValidateAndThrow(request); //ValidationException
            var category = _mapper.Map<Category>(request);
            Context.Add(category);
            Context.SaveChanges();
        }
    }
}
