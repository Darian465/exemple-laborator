using System;
using System.Runtime.Serialization;

namespace ShoppingCarts.Domain.Models
{

    [Serializable]
    public class InvalidatedCustomerOrder : Exception
    {
        public InvalidatedCustomerOrder()
        {
        }

        public InvalidatedCustomerOrder(string? message) : base(message)
        {
        }

        public InvalidatedCustomerOrder(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public InvalidatedCustomerOrder(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
