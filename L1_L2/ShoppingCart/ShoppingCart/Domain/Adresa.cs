using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShoppingCart.Domain
{
    public record Adresa
    {
        private static readonly Regex ValidAdresa = new("^Str. ");
        public string Value { get; }
        public Adresa(string value)
        {
            if (IsValid(value))
            {
                Value = value;
            }
            else
            {
                throw new InvalidAdresaException("Adresa invalida");
            }
        }
        private static bool IsValid(string stringValue) => ValidAdresa.IsMatch(stringValue);
        public override string ToString()
        {
            return Value;
        }
        public static bool TryParseAdresa(string stringValue, out Adresa adresa)
        {
            bool isValid = false;
            adresa = null;

            if (IsValid(stringValue))
            {
                isValid = true;
                adresa = new(stringValue);
            }
            return isValid;
        }
    }
}
