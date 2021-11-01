using System;
using System.Runtime.Serialization;

namespace ShoppingCart.Domain
{
    [Serializable]
    internal class InvalidAdresaException : Exception
    {
        public InvalidAdresaException()
        {
        }

        public InvalidAdresaException(string message) : base(message)
        {
        }

        public InvalidAdresaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidAdresaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}