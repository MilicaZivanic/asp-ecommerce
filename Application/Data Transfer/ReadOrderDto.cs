using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data_Transfer
{
    public class ReadOrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string AddressInfo { get; set; }
        public string PaymentInfo { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public string UserInfo { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
        public decimal Total { get; set; }
    }
}
