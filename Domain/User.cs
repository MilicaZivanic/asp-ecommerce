using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual ICollection<UserAddress> UserAddresses { get; set; } = new HashSet<UserAddress>();
        public virtual ICollection<UserPayment> UserPayments { get; set; } = new HashSet<UserPayment>();
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public virtual ICollection<UserUseCase> UserUseCases { get; set; } = new HashSet<UserUseCase>();


    }
}
