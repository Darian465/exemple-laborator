using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShoppingCart.Domain.Cart;
using ShoppingCart.Domain;

namespace ShoppingCart
{
    class CartOperation
    {
        public static ICart ValidateCart(Func<Cod, bool> checkCodExist, GolCart cart)
        {
            List<ValidatedProdus> validatedCantitate = new();
            bool isValidList = true;
            string invalidReason = string.Empty;
            foreach(var unvalidatedCantitate in cart.ProdusList)
            {
                if(!Cantitate.TryParseCantitate(unvalidatedCantitate.Cart, out Cantitate cantitate))
                {
                    invalidReason = $"Invalida cantitatea ({unvalidatedCantitate.Cod},{unvalidatedCantitate.Cart})";
                    isValidList = false;
                    break;
                }
                if (!Cantitate.TryParseCantitate(unvalidatedCantitate.Cart, out Cantitate activityCantitate))
                {
                    invalidReason = $"Invalida cantitatea ({unvalidatedCantitate.Cod},{unvalidatedCantitate.ActivityCantitate})";
                    isValidList = false;
                    break;
                }
                if(!Cod.TryParse(unvalidatedCantitate.Cod, out Cod cod))
                {
                    invalidReason = $"Invalid cod ({unvalidatedCantitate.Cod})";
                    isValidList = false;
                    break;
                }
                if(!Adresa.TryParseAdresa(unvalidatedCantitate.Adresa, out Adresa adresa))
                {
                    invalidReason = $"Invalida adresa ({unvalidatedCantitate.Adresa})";
                    isValidList = false;
                    break;
                }
                ValidatedProdus validProdus = new(cod, cantitate, adresa, activityCantitate);
                validatedCantitate.Add(validProdus);
            }

            if (isValidList)
            {
                return new ValidareCart(validatedCantitate);
            }
            else
            {
                return new NevalidatCart(cart.ProdusList, invalidReason);
            }
            
        }
        public static ICart CalculateFinalCantitate(ICart cantitate) => cantitate.Match(
            whenGolCart: golCart => golCart,
            whenNevalidatCart: nevalidatCart => nevalidatCart,
            whenCalculareCart: calculareCart => calculareCart,
            whenPlatireCart: platireCart => platireCart,
            whenValidareCart: validCart =>
            {
            var calculareCantitate = validCart.ProdusList.Select(validProdus =>
                new CalculatedProdus(validProdus.Cod,
                                validProdus.Cart,
                                validProdus.Adresa,
                                validProdus.ActivityCantitate,
                                validProdus.ActivityCantitate * validProdus.Cart));
                return new CalculareCart(calculareCantitate.ToList().AsReadOnly());
            }
        );
        public static ICart PublishCart(ICart cantitate) => cantitate.Match(
            whenGolCart: golcart => golcart,
            whenNevalidatCart: nevalidatCart => nevalidatCart,
            whenValidareCart: validareCart => validareCart,
            whenPlatireCart: platireCart => platireCart,
            whenCalculareCart: calculareCart =>
            {
                StringBuilder csv = new();
                calculareCart.ProdusList.Aggregate(csv, (export, cantitate) => export.AppendLine($"{cantitate.Cod.Value},{cantitate.Cantitate},{cantitate.ActivityCantitate},{cantitate.FinalCantitate}"));

                PlatireCart platireCart = new(calculareCart.ProdusList, csv.ToString(), DateTime.Now);

                return platireCart;
            }
        );
    }
}
