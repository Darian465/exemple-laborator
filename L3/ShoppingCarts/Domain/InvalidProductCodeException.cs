using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ShoppingCarts.Domain
{
    [Serializable]
    internal class InvalidProductCodeException : Exception
    {
        private IReadOnlyCollection<EmptyShoppingCart> shoppingCartList;
        private string errorMessage;

        public InvalidProductCodeException()
        {
        }

        public InvalidProductCodeException(string message) : base(message)
        {
        }

        public InvalidProductCodeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidProductCodeException(IReadOnlyCollection<EmptyShoppingCart> shoppingCartList, string errorMessage)
        {
            this.shoppingCartList = shoppingCartList;
            this.errorMessage = errorMessage;
        }

        protected InvalidProductCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}