using Application.Data_Transfer;
using Application.Exceptions;
using Application.Queries;
using AutoMapper;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class EfGetDiscountQuery : EfQuery, IGetDiscountQuery
    {
        private readonly IMapper _mapper;
        public EfGetDiscountQuery(EcomShopContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 26;

        public string Name => "Ef Get Discount";

        public DiscountDto Execute(int search)
        {
            var discount = Context.Discounts.Find(search);

            if (discount == null)
            {
                throw new EntityNotFoundException(search, typeof(Discount));
            }

            return _mapper.Map<DiscountDto>(discount);
        }
    }
}
