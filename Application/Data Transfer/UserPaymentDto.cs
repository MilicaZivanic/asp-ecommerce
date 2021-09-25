using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data_Transfer
{
    public class UserPaymentDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PaymentType { get; set; }
        public string Provider { get; set; }
        public int AccountNumber { get; set; }
        public DateTime Expiry { get; set; }
    }
}
