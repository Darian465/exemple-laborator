using System;
using System.Runtime.Serialization;

namespace ShoppingCart.Domain
{
    [Serializable]
    internal class InvalidCantitateException : Exception
    {
        public InvalidCantitateException()
        {
        }

        public InvalidCantitateException(string message) : base(message)
        {
        }

        public InvalidCantitateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidCantitateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}