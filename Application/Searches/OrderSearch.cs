using Application.Queries;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Searches
{
    public class OrderSearch : PagedSearch
    {
        public int? MinOrderLines { get; set; }
        public OrderStatus? Status { get; set; }
        public decimal? MinPrice { get; set; }
    }
}
