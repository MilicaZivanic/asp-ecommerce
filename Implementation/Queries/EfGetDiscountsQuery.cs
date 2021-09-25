using Application.Data_Transfer;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class EfGetDiscountsQuery : EfQuery,IGetDiscountsQuery
    {
        private readonly IMapper _mapper;
        public EfGetDiscountsQuery(EcomShopContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 27;

        public string Name => "Ef Get Discounts";

        public IEnumerable<DiscountDto> Execute(DiscountSearch search)
        {
            var discounts = Context.Discounts.AsQueryable();

            if (!string.IsNullOrEmpty(search.Name))
            {
                discounts = discounts.Where(x => x.Name.Contains(search.Name));
            }
            if (!string.IsNullOrEmpty(search.Description))
            {
                discounts = discounts.Where(x => x.Description.Contains(search.Name));
            }
            if (search.DiscountPercent.HasValue)
            {
                discounts = discounts.Where(x => x.DiscountPercent == search.DiscountPercent);
            }

            return _mapper.Map<List<DiscountDto>>(discounts.ToList());
        }
    }
}
