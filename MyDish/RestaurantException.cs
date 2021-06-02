using System;


namespace MyRestaurant
{
    public class RestaruantException : Exception
    {
        public RestaruantException(string message)
            : base(message)
        {
        }
    }
}

