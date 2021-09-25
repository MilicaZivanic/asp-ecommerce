using Application.Data_Transfer;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class EfGetOrdersQuery : EfQuery, IGetOrdersQuery
    {
        private readonly IMapper _mapper;
        public EfGetOrdersQuery(EcomShopContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 16;

        public string Name => "Ef Get Orders";

        public PagedResponse<OrderDto> Execute(OrderSearch search)
        {
            var orders = Context.Orders.Include(x => x.OrderItems).Include(x => x.User).AsQueryable();

            if (search.MinOrderLines.HasValue)
            {
                orders = orders.Where(order => order.OrderItems.Count() >= search.MinOrderLines.Value);
            }

            if (search.Status.HasValue)
            {
                orders = orders.Where(o => o.OrderStatus == search.Status);
            }

            if (search.MinPrice.HasValue)
            {
                orders = orders.Where(o => o.OrderItems.Sum(x => x.Quantity * x.Price) >= search.MinPrice.Value);
            }

            var skip = (search.Page - 1) * search.PerPage;

            return new PagedResponse<OrderDto>
            {
                TotalCount = orders.Count(),
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                Items = _mapper.Map<IEnumerable<OrderDto>>(orders.Skip(skip).Take(search.PerPage).ToList())
            };
        }
    }
}
