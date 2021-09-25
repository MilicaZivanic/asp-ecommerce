using Application.Commands;
using Application.Commands.ProductCommands;
using Application.Exceptions;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.ProductCommands
{
    public class EfDeleteProductCommand : EfCommand, IDeleteProductCommand
    {
        public EfDeleteProductCommand(EcomShopContext context) : base(context)
        {
        }

        public int Id => 12;

        public string Name => "Ef Delete Product";

        public void Execute(int request)
        {
            var product = Context.Products.Find(request);

            if (product == null)
            {
                throw new EntityNotFoundException(request, typeof(Product));
            }

            product.IsDeleted = true;
            product.IsActive = false;
            product.DeletedAt = DateTime.Now;

            Context.SaveChanges();
        }
    }
}
