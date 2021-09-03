using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class UserPayment : Entity
    {
        public int UserId { get; set; }
        public string PaymentType { get; set; }
        public string Provider { get; set; }
        public int AccountNumber { get; set; }
        public DateTime Expiry { get; set; }

        public virtual User User { get; set; }
    }
}
