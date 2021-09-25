using Application;
using Application.Commands.OrderCommands;
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

namespace Implementation.Commands.OrderCommands
{
    public class EfCreateOrderCommand : EfCommand, ICreateOrderCommand
    {
        private readonly CreateOrderValidator _validator;
        private readonly IMapper _mapper;
        private readonly IApplicationActor _actor;
        public EfCreateOrderCommand(EcomShopContext context, CreateOrderValidator validator, IMapper mapper, IApplicationActor actor) : base(context)
        {
            _validator = validator;
            _mapper = mapper;
            _actor = actor;
        }

        public int Id => 18;

        public string Name => "Ef Create Order";

        public void Execute(OrderDto request)
        {
            _validator.ValidateAndThrow(request);
            var order = new Order
            {
                UserId = _actor.Id,
                Address = request.Address,
                City = request.City,
                Country = request.Country,
                PostalCode = request.PostalCode,
                AccountNumber = request.AccountNumber,
                OrderItems = request.OrderItems.Select(x =>
                {
                    var product = Context.Products.Find(x.ProductId);
                    product.Quantity -= x.Quantity;
                    return new OrderItem
                    {
                        ProductName = product.Name,
                        Price = product.Price,
                        ProductId = product.Id,
                        Quantity = x.Quantity
                    };

                }).ToList(),
                Total = request.OrderItems.Sum(x =>
                {
                    var product = Context.Products.Find(x.ProductId);
                    var total = x.Quantity * product.Price;
                    return total;
                })
            };

            Context.Orders.Add(order);
            Context.SaveChanges();
        }
    }
}
