using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Order : Entity
    {
        public int UserId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public int AccountNumber { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal Total { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }
    public enum OrderStatus
    {
        Recieved,
        Delivered,
        Shipped,
        Canceled
    }
}
