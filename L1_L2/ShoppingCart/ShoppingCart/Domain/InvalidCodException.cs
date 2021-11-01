using System;
using System.Runtime.Serialization;

namespace ShoppingCart.Domain
{
    [Serializable]
    internal class InvalidCodException : Exception
    {
        public InvalidCodException()
        {
        }

        public InvalidCodException(string message) : base(message)
        {
            Console.WriteLine("Cod invalid");
        }

        public InvalidCodException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidCodException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}