using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class EfQuery
    {
        private readonly EcomShopContext _context;

        public EfQuery(EcomShopContext context)
        {
            _context = context;
        }

        protected EcomShopContext Context => _context;
    }
}
