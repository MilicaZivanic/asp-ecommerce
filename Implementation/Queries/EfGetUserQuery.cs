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
    public class EfGetUserQuery : EfQuery, IGetUserQuery
    {
        private readonly IMapper _mapper;
        public EfGetUserQuery(EcomShopContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 13;

        public string Name => "Ef Get User";

        public UserDto Execute(int search)
        {
            var user = Context.Users.Include(x => x.UserAddresses).Include(x => x.UserPayments).FirstOrDefault(x => x.Id == search);

            if (user == null)
            {
                throw new EntityNotFoundException(search, typeof(User));
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
