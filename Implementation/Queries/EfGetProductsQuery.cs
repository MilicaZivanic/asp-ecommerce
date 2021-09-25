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
    public class EfGetProductsQuery : EfQuery, IGetProductsQuery
    {
        private readonly IMapper _mapper;
        public EfGetProductsQuery(EcomShopContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 8;

        public string Name => "Ef Get Products - Paged Response";

        public PagedResponse<ProductDto> Execute(ProductSearch search)
        {
            var productsQuery = Context.Products.Include(x => x.Category).Include(x => x.Discount).AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                search.Keyword = search.Keyword.ToLower();

                productsQuery = productsQuery.Where(x =>
                                                        x.Name.ToLower().Contains(search.Keyword) ||
                                                        x.Description.ToLower().Contains(search.Keyword));
            }

            if (search.CategoryId.HasValue)
            {
                productsQuery = productsQuery.Where(x => x.CategoryId == search.CategoryId);
            }
            if (search.DiscountId.HasValue)
            {
                productsQuery = productsQuery.Where(x => x.DiscountId == search.DiscountId);
            }
            if (search.MinPrice.HasValue)
            {
                productsQuery = productsQuery.Where(x => x.Price >= search.MinPrice);
            }

            if (search.MaxPrice.HasValue)
            {
                productsQuery = productsQuery.Where(x => x.Price <= search.MaxPrice);
            }

            var skip = (search.Page - 1) * search.PerPage;

            return new PagedResponse<ProductDto>
            {
                TotalCount = productsQuery.Count(),
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                Items = _mapper.Map<List<ProductDto>>(productsQuery.Skip(skip).Take(search.PerPage).ToList())
            };

        }
    }
}
