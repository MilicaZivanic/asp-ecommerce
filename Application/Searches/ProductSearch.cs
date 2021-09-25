using Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Searches
{
    public class ProductSearch : PagedSearch
    {
        public string Keyword { get; set; }
        public int? CategoryId { get; set; }
        public int? DiscountId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

    }
}
