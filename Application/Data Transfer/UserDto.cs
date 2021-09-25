using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data_Transfer
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }

        public IEnumerable<UserAddressDto> Addresses { get; set; } = new List<UserAddressDto>();
        public IEnumerable<UserPaymentDto> Payments { get; set; } = new List<UserPaymentDto>();
    }
}
