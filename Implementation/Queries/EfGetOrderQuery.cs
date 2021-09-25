using Application.Data_Transfer;
using Application.Exceptions;
using Application.Queries;
using AutoMapper;
using Domain;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class EfGetOrderQuery : EfQuery, IGetOrderQuery
    {
        private readonly IMapper _mapper;
        public EfGetOrderQuery(EcomShopContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 17;

        public string Name => "Ef Get Query";

        public ReadOrderDto Execute(int search)
        {
            var order = Context.Orders.Include(x => x.OrderItems).Include(x => x.User).FirstOrDefault(x => x.Id == search);

            if (order == null)
            {
                throw new EntityNotFoundException(search, typeof(Order));
            }

            return _mapper.Map<ReadOrderDto>(order);
        }
    }
}
