using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShoppingCart.Domain
{
    public record Cod
    {
        private static readonly Regex ValidCod = new("^P[0-9]{5}$");
        public string Value { get; }
        public Cod(string value)
        {
            if(IsValid(value))
            {
                Value = value;
            }
            else
            {
                throw new InvalidCodException("Invalid");
            }
        }
        private static bool IsValid(string stringValue) => ValidCod.IsMatch(stringValue);

        public override string ToString()
        {
            return Value;
        }
        public static bool TryParse(string stringValue, out Cod cod)
        {
            bool isValid = false;
            cod = null;
            if (IsValid(stringValue))
            {
                isValid = true;
                cod = new(stringValue);
            }
            return isValid;
        }

    }
}
