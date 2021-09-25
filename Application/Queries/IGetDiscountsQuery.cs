using Application.Data_Transfer;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public interface IGetDiscountsQuery : IQuery<DiscountSearch, IEnumerable<DiscountDto>>
    {
    }
}
