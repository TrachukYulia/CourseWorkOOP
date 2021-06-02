using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRestaurant
{
    public class OrderException : Exception
    {
        public OrderException(string message)
            : base(message)
        { }
        public OrderException(string message, string parameter)
            : base(message) { }
    }
}
