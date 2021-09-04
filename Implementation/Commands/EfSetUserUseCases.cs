using Application.Commands;
using Application.Data_Transfer;
using Application.Exceptions;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public class EfSetUserUseCases : ISetUserUseCase
    {
        private readonly EcomShopContext _context;

        public EfSetUserUseCases(EcomShopContext context)
        {
            _context = context;
        }

        public int Id => 1;

        public string Name => "Ef Change User Use Cases";

        public void Execute(UserUseCaseDto request)
        {
            if (!_context.Users.Any(x => x.Id == request.UserId))
            {
                throw new EntityNotFoundException(request.UserId, typeof(User));
            }

            var useCasesToRemove = _context.UserUseCases.Where(x => x.UserId == request.UserId);
            _context.UserUseCases.RemoveRange(useCasesToRemove);

            _context.UserUseCases.AddRange(request.AllowedUseCases.Select(x => new UserUseCase
            {
                UserId = request.UserId,
                UserCaseId = x
            }));

            _context.SaveChanges();
        }
    }
}
