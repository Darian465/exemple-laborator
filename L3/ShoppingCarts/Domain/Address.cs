using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace ShoppingCarts.Domain
{
    public record Address
    {
        private static readonly Regex ValidPattern = new("^.*$");

        public string _address { get; }

        public Address(string address)
        {
            if (ValidPattern.IsMatch(address))
            {
                _address = address;
            }
            else
            {
                throw new InvalidAddressException("");
            }
        }

        public override string ToString()
        {
            return _address;
        }

        public static Option<Address> TryParse(string addressStrings)
        {
            if (IsValid(addressStrings))
            {
                return Some<Address>(new(addressStrings));
            }
            else
            {
                return None;
            }
           
        }
        private static bool IsValid(string stringValue) => ValidPattern.IsMatch(stringValue);

    }
}
