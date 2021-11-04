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
    public record ProductCode
    {
        private static readonly Regex ValidPattern = new("^.*$");

        public string Code { get; }

        public ProductCode(string value)
        {
            if (ValidPattern.IsMatch(value))
            {
                Code = value;
            }
            else
            {
                throw new InvalidProductCodeException("");
            }
        }

        public override string ToString()
        {
            return Code;
        }

        private static bool IsValid(string stringValue) => ValidPattern.IsMatch(stringValue);

        public static Option<ProductCode> TryParse(string productCodeString)
        {
            
            if (IsValid(productCodeString))
            {
                return Some<ProductCode>(new(productCodeString));
            }
            else
            {
                return None;
            }
        }
    }
}
