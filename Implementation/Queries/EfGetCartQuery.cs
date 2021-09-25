using Application.Data_Transfer;
using Application.Queries;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class EfGetCartQuery : EfQuery, IGetCartQuery
    {
        public EfGetCartQuery(EcomShopContext context) : base(context)
        {
        }

        public int Id => 31;

        public string Name => throw new NotImplementedException();

        public CartDto Execute(int? search)
        {
            throw new NotImplementedException();
        }
    }
}
