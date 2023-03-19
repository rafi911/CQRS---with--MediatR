using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class OrderLineDomainException : Exception
    {
        public OrderLineDomainException()
        {
        }

        public OrderLineDomainException(string message) : base(message)
        {
        }

        public OrderLineDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
