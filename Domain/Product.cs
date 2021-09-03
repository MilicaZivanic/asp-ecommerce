using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public int? DiscountId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Discount Discount { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

    }
}
