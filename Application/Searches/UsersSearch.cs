using Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Searches
{
    public class UsersSearch : PagedSearch
    {
        public string Keyword { get; set; }
    }
}
