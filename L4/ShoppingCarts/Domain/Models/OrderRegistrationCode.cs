using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LanguageExt;
using static LanguageExt.Prelude;

namespace ShoppingCarts.Domain.Models
{
    public record OrderRegistrationCode
    {
        public static readonly Regex validCode = new("^RO[0-9]{5}$");
        public string Value { get; }

        public OrderRegistrationCode(string value)
        {
            if (IsValid(value))
            {
                Value = value;
            }
            else
            {
                throw new InvalidOrderRegistrationNumberException("");
            }
        }

        private static bool IsValid(string stringValue) => validCode.IsMatch(stringValue);

        public override string ToString()
        {
            return Value;
        }

        public static bool TryParse(string stringValue, out OrderRegistrationCode registrationNumber)
        {
            bool isValid = false;
            registrationNumber = null;

            if (IsValid(stringValue))
            {
                isValid = true;
                registrationNumber = new(stringValue);
            }

            return isValid;
        }

        public static Option<OrderRegistrationCode> TryParseRegistrationCode(string stringValue)
        {
            if (IsValid(stringValue))
            {
                return Some<OrderRegistrationCode>(new(stringValue));
            }
            else
            {
                return None;
            }
        }
    }
}
