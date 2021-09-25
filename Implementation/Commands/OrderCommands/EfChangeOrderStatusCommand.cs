using Application.Commands.OrderCommands;
using Application.Data_Transfer;
using Application.Exceptions;
using Domain;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.OrderCommands
{
    public class EfChangeOrderStatusCommand : EfCommand, IChangeOrderStatusCommand
    {
        public EfChangeOrderStatusCommand(EcomShopContext context) : base(context)
        {
        }

        public int Id => 19;

        public string Name => "Ef Change Order Status";

        public void Execute(ChangeOrderStatusDto request)
        {
            var order = Context.Orders.Include(o => o.OrderItems).ThenInclude(ol => ol.Product).FirstOrDefault(x => x.Id == request.OrderId);

            if (order == null)
            {
                throw new EntityNotFoundException(request.OrderId, typeof(Order));
            }

            if (order.OrderStatus == OrderStatus.Delivered)
            {
                throw new ConflictException("Can not change status of delevered order.");
            }

            if (order.OrderStatus == OrderStatus.Recieved || order.OrderStatus == OrderStatus.Shipped)
            {
                if (request.Status == OrderStatus.Canceled || request.Status == OrderStatus.Shipped)
                {
                    order.OrderStatus = request.Status;

                    if (request.Status == OrderStatus.Canceled)
                    {
                        foreach (var line in order.OrderItems)
                        {
                            line.Product.Quantity += line.Quantity;
                        }
                    }
                    Context.SaveChanges();
                }
                else
                {
                    throw new ConflictException("Order can't be transitioned from recieved to delivered directly.");
                }


            }
        }
    }
}
