using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace ShoppingCarts.Domain
{
    public record Quantity
    {
        public int Value { get; }

        public Quantity(int value)
        {
            if (IsValid(value))
            {
                Value = value;
            }
            else
            {
                throw new InvalidQuantityException($"{value} is an invalid quantity value.");
            }
        }

        public override string ToString()
        {
            return $"{Value}";
        }

        public static Option<Quantity> TryParse(string quantityString)
        {
            if (int.TryParse(quantityString, out int numericQuantity) && IsValid(numericQuantity))
            {
                return Some<Quantity>(new (numericQuantity));
            }
            else
            {
                return None;
            }
        }

        private static bool IsValid(int numericQuantity) => numericQuantity > 0;
    }
}
