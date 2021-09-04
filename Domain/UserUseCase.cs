using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class UserUseCase : Entity
    {
        public int UserId { get; set; }
        public int UserCaseId { get; set; }
        public virtual User User { get; set; }

    }
}
