using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Cart : Entity
    {
        public int? UserId { get; set; }
        public decimal Total { get; set; }
        public virtual User User { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
    }
}
