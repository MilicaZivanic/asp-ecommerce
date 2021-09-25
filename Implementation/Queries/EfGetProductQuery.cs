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
    public class EfGetProductQuery : EfQuery,IGetProductQuery
    {
        private readonly IMapper _mapper;
        public EfGetProductQuery(EcomShopContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 9;

        public string Name => "Ef Get One Product";

        public ProductDto Execute(int search)
        {
            var product = Context.Products.Include(x => x.Category).Include(x => x.Discount).FirstOrDefault(x => x.Id == search);

            if (product == null)
            {
                throw new EntityNotFoundException(search, typeof(Product));
            }

            return _mapper.Map<ProductDto>(product);
        }
    }
}
