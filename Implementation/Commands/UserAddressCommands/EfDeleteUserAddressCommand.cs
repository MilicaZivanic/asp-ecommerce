using Application;
using Application.Commands.UserAddressCommands;
using Application.Exceptions;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.UserAddressCommands
{
    public class EfDeleteUserAddressCommand : EfCommand, IDeleteUserAddressCommand
    {
        private readonly IApplicationActor _actor;

        public EfDeleteUserAddressCommand(EcomShopContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 22;

        public string Name => "Ef Delete Address For A User";

        public void Execute(int request)
        {
            var address = Context.UserAddresses.Where(x => x.UserId == _actor.Id).FirstOrDefault(x => x.Id == request);

            if (address == null)
            {
                throw new EntityNotFoundException(request, typeof(UserAddress));
            }

            address.IsDeleted = true;
            address.IsActive = false;
            address.DeletedAt = DateTime.Now;

            Context.SaveChanges();
        }
    }
}
