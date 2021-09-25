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
    public class EfGetCategoriesQuery : EfQuery, IGetCategoriesQuery
    {
        private readonly IMapper _mapper;
        public EfGetCategoriesQuery(EcomShopContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 4;

        public string Name => "Ef search categories";

        public IEnumerable<CategoryDto> Execute(CategorySearch search)
        {
            var categories = Context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(search.Name))
            {
                categories = categories.Where(x => x.Name.Contains(search.Name));
            }

            return _mapper.Map<List<CategoryDto>>(categories.ToList());
        }
    }
}
