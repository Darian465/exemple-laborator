using System;
using System.Runtime.Serialization;

namespace ShoppingCarts.Domain.Models
{
    [Serializable]
    internal class InvalidOrderRegistrationNumberException : Exception
    {
        public InvalidOrderRegistrationNumberException()
        {
        }

        public InvalidOrderRegistrationNumberException(string? message) : base(message)
        {
        }

        public InvalidOrderRegistrationNumberException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidOrderRegistrationNumberException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
