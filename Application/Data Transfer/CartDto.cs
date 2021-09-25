using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data_Transfer
{
    public class CartDto 
    {
        public int? UserId { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
    }
}
