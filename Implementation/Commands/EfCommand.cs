using AutoMapper;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public class EfCommand
    {
        private readonly EcomShopContext _context;

        public EfCommand(EcomShopContext context)
        {
            _context = context;
        }

        protected EcomShopContext Context => _context;
    }
}
