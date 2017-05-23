using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class NameAlreadyUsedException : Exception
    {
        public NameAlreadyUsedException(string message) : base(message) { }
    }
}
