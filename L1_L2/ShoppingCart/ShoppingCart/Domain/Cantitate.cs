using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Domain
{
   public record Cantitate
   {
        public decimal Value { get; }
        public Cantitate(decimal value)
        {
            if (IsValid(value))
            {
                Value = value;
            }
            else
            {
                throw new InvalidCantitateException($"{value:#.##} este invalid.");
            }
        }
        public static Cantitate operator *(Cantitate a, Cantitate b) => new Cantitate(a.Value * b.Value);
        public Cantitate Round()
        {
            var roundeValue = Math.Round(Value);
            return new Cantitate(roundeValue);
        }

        public override string ToString()
        {
            return $"{Value:0.##}";
        }
        public static bool TryParseCantitate(string cantitateString, out Cantitate cantitate)
        {
            bool isValid = false;
            cantitate = null;
            if(decimal.TryParse(cantitateString, out decimal numericCantitate))
            {
                if (IsValid(numericCantitate))
                {
                    isValid = true;
                    cantitate = new(numericCantitate);
                }
            }
            return isValid;
        }
        private static bool IsValid(decimal numericCantitate) => numericCantitate > 0; 
    }
}
