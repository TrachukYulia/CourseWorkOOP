using System;

namespace MyRestaurant
{
    public class DishException : Exception
    {
        public DishException(string message)
            : base(message)
        { }
        public DishException (string message, string parameter)
            :base(message) { }
    }
}
