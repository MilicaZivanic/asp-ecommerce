using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string message = null)
            :base($"There was an conflict. You can't perform this operation.")
        {

        }
    }
}
