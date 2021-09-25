using Application.Commands;
using Application.Commands.CategoryCommands;
using Application.Exceptions;
using AutoMapper;
using Domain;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.CategoryCommands
{
    public class EfDeleteCategoryCommand : EfCommand, IDeleteCategoryCommand
    {
        public EfDeleteCategoryCommand(EcomShopContext context) : base(context)
        {
        }

        public int Id => 6;

        public string Name => "Ef Delete Category";

        public void Execute(int request)
        {
            var category = Context.Categories.Include(x => x.Products).FirstOrDefault(x => x.Id == request);

            if (category == null)
            {
                throw new EntityNotFoundException(request, typeof(Category));
            }
            if (category.Products.Any())
            {
                throw new ConflictException("Some of the products belong to this category, there for you are not allowed to delete it.");
            }

            category.IsDeleted = true;
            category.IsActive = false;
            category.DeletedAt = DateTime.Now;

            Context.SaveChanges();
        }
    }
}
