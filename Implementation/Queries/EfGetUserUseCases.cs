using Application.Data_Transfer;
using Application.Queries;
using Application.Searches;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class EfGetUserUseCases : EfQuery,IGetUserUseCases
    {
        public EfGetUserUseCases(EcomShopContext context) : base(context)
        {
        }

        public int Id => 3;

        public string Name => "Ef Search User Use Cases";

        public IEnumerable<UserUseCaseDto> Execute(UserUseCaseSearch search)
        {
            var users = Context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                var keyword = search.Keyword.ToLower();

                users = users.Where(x => x.Email.Contains(keyword) || x.FirstName.Contains(keyword) || x.LastName.Contains(keyword));
            }

            var userList = users.ToList();

            var userUseCases = new List<UserUseCaseDto>();

            var userUseCasesDb = Context.UserUseCases.Where(x => userList.Select(y => y.Id).Contains(x.UserId));

            foreach (var user in userList)
            {
                var useCaseIds = userUseCasesDb.Where(x => x.UserId == user.Id).Select(x => x.UseCaseId).ToList();

                userUseCases.Add(new UserUseCaseDto
                {
                    AllowedUseCases = useCaseIds,
                    Identity = user.Email,
                    UserId = user.Id
                });
            }

            return userUseCases;
        }
    }
}
