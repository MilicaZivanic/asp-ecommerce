using Application.Data_Transfer;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class EfGetUsersQuery : EfQuery, IGetUsersQuery
    {
        private readonly IMapper _mapper;
        public EfGetUsersQuery(EcomShopContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 12;

        public string Name => "Ef Search users";

        public PagedResponse<UserDto> Execute(UsersSearch search)
        {
            var usersQuery = Context.Users.Include(x => x.UserAddresses).Include(x => x.UserPayments).AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                search.Keyword = search.Keyword.ToLower();

                usersQuery = usersQuery.Where(x =>
                                              x.FirstName.ToLower().Contains(search.Keyword) ||
                                              x.LastName.ToLower().Contains(search.Keyword) ||
                                              x.Email.ToLower().Contains(search.Keyword));
            }

            var skip = (search.Page - 1) * search.PerPage;

            return new PagedResponse<UserDto>
            {
                TotalCount = usersQuery.Count(),
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                Items = _mapper.Map<List<UserDto>>(usersQuery.Skip(skip).Take(search.PerPage).ToList())
            };            
        }
    }
}
