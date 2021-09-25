using Application;
using Application.Commands.UserPaymentCommands;
using Application.Exceptions;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.UserPaymentCommands
{
    public class EfDeleteUserPaymentCommand : EfCommand, IDeleteUserPaymentCommand
    {
        private readonly IApplicationActor _actor;

        public EfDeleteUserPaymentCommand(EcomShopContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 24;

        public string Name => "Ef Delete Payment For A User";

        public void Execute(int request)
        {
            var payment = Context.UserPayments.Where(x => x.UserId == _actor.Id).FirstOrDefault(x => x.Id == request);

            if (payment == null)
            {
                throw new EntityNotFoundException(request, typeof(UserPayment));
            }

            payment.IsDeleted = true;
            payment.IsActive = false;
            payment.DeletedAt = DateTime.Now;

            Context.SaveChanges();
        }
    }
}
