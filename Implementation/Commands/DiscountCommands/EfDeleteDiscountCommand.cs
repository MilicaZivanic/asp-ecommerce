using Application.Commands.DiscountCommands;
using Application.Exceptions;
using Domain;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.DiscountCommands
{
    public class EfDeleteDiscountCommand : EfCommand, IDeleteDiscountCommand
    {
        public EfDeleteDiscountCommand(EcomShopContext context) : base(context)
        {
        }

        public int Id => 30;

        public string Name => "Ef Delete Discount";

        public void Execute(int request)
        {
            var discount = Context.Discounts.Include(x => x.Products).FirstOrDefault(x => x.Id == request);

            if (discount == null)
            {
                throw new EntityNotFoundException(request, typeof(Discount));
            }
            if (discount.Products.Any())
            {
                throw new ConflictException("Some of the products have this discount, there for you are not allowed to delete it.");
            }

            discount.IsDeleted = true;
            discount.IsActive = false;
            discount.DeletedAt = DateTime.Now;

            Context.SaveChanges();
        }
    }
}
