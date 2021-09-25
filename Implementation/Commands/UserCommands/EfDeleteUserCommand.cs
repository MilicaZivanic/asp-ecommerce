using Application.Commands.UserCommands;
using Application.Exceptions;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.UserCommands
{
    public class EfDeleteUserCommand : EfCommand, IDeleteUserCommand
    {
        public EfDeleteUserCommand(EcomShopContext context) : base(context)
        {
        }

        public int Id => 14;

        public string Name => "Ef Delete User";

        public void Execute(int request)
        {
            var user = Context.Users.Find(request);

            if (user == null)
            {
                throw new EntityNotFoundException(request, typeof(User));
            }

            user.IsDeleted = true;
            user.IsActive = false;
            user.DeletedAt = DateTime.Now;

            Context.SaveChanges();
        }
    }
}
