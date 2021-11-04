using LanguageExt;
using ShoppingCarts.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShoppingCarts.Domain.ShoppingCarts;
using static LanguageExt.Prelude;

namespace ShoppingCarts
{
    public static class ShoppingCartsOperations
    {
        public static Task<IShoppingCarts> ValidateShoppingCarts(Func<ProductCode, TryAsync<bool>> checkProductExists, EmptyShoppingCarts shoppingCarts) =>
            shoppingCarts.ShoppingCartList
            .Select(ValidateProductCode(checkProductExists))
            .Aggregate(CreateEmptyValatedList().ToAsync(), ReduceValid)
            .MatchAsync(
                Right: validatedShoppingCarts => new ValidatedShoppingCarts(validatedShoppingCarts),
                LeftAsync: errorMessage => Task.FromResult((IShoppingCarts)new InvalidProductCodeException(shoppingCarts.ShoppingCartList, errorMessage))
                );

        private static Func<EmptyShoppingCart, EitherAsync<string, ValidatedShoppingCart>> ValidateProductCode(Func<ProductCode, TryAsync<bool>> checkProductExists) =>
            emptyShoppingCart => ValidateProductCode(checkProductExists, emptyShoppingCart);

        private static EitherAsync<string, ValidatedShoppingCart> ValidateProductCode(Func<ProductCode, TryAsync<bool>> checkProductExists, EmptyShoppingCart emptyShoppingCart) =>
            from productCode in ProductCode.TryParse(emptyShoppingCart.productCode)
                                            .ToEitherAsync(() => $"Invalid product code ({emptyShoppingCart.productCode})")
            from quantity in Quantity.TryParse(emptyShoppingCart.quantity)
                                            .ToEitherAsync(() => $"Invalid quantity ({emptyShoppingCart.productCode}, {emptyShoppingCart.quantity})")
            from address in Address.TryParse(emptyShoppingCart.address)
                                            .ToEitherAsync(() => $"Invalid address ({emptyShoppingCart.productCode}, {emptyShoppingCart.address})")
            from price in Price.TryParse(emptyShoppingCart.price)
                                            .ToEitherAsync(() => $"Invalid price ({emptyShoppingCart.productCode}, {emptyShoppingCart.price})")
            select new ValidatedShoppingCart(productCode, quantity, address, price);

        private static Either<string, List<ValidatedShoppingCart>> CreateEmptyValatedList() =>
            Right(new List<ValidatedShoppingCart>());

        private static EitherAsync<string, List<ValidatedShoppingCart>> ReduceValid(EitherAsync<string, List<ValidatedShoppingCart>> acc, EitherAsync<string, ValidatedShoppingCart> next) =>
            from list in acc
            from nextt in next
            select list.AppendValid(nextt);

        private static List<ValidatedShoppingCart> AppendValid(this List<ValidatedShoppingCart> list, ValidatedShoppingCart validated)
        {
            list.Add(validated);
            return list;
        }



        public static IShoppingCarts CalculateFinalPrice(IShoppingCarts shoppingCarts) => shoppingCarts.Match(
            whenEmptyShoppingCarts: emptyShoppingCart => emptyShoppingCart,
            whenUnvalidatedShoppingCarts: unvalidatedShoppingCart => unvalidatedShoppingCart,
            whenCalculatedShoppingCarts: calculatedShoppingCart => calculatedShoppingCart,
            whenPaidShoppingCarts: paidShoppingCart => paidShoppingCart,
            whenValidatedShoppingCarts: CalculatePrice

        );
        private static IShoppingCarts CalculatePrice(ValidatedShoppingCarts validated) =>
            new CalculatedShoppingCarts(validated.ShoppingCartList
                                        .Select(CalculatePriceFinal)
                                        .ToList()
                                        .AsReadOnly()
                );
        private static CalculatedShoppingCart CalculatePriceFinal(ValidatedShoppingCart validated) =>
            new CalculatedShoppingCart(validated.productCode,
                                        validated.quantity,
                                        validated.address,
                                        validated.price,
                                        validated.price * validated.quantity
                );


        public static IShoppingCarts PayShoppingCart(IShoppingCarts shoppingCarts) => shoppingCarts.Match(
                whenEmptyShoppingCarts: emptyShoppingCart => emptyShoppingCart,
                whenUnvalidatedShoppingCarts: unvalidatedShoppingCart => unvalidatedShoppingCart,
                whenPaidShoppingCarts: paidShoppingCart => paidShoppingCart,
                whenValidatedShoppingCarts: validShoppingCarts => validShoppingCarts,
                whenCalculatedShoppingCarts: GenerateExport
        );

        private static IShoppingCarts GenerateExport(CalculatedShoppingCarts calculatedShoppingCarts) =>
            new PaidShoppingCarts(calculatedShoppingCarts.ShoppingCartList,
                                    calculatedShoppingCarts.ShoppingCartList.Aggregate(new StringBuilder(), CreateCsvLine).ToString(),
                                    DateTime.Now
                );
        private static StringBuilder CreateCsvLine(StringBuilder export, CalculatedShoppingCart calculated) =>
            export.AppendLine($"{calculated.productCode}, {calculated.quantity}, {calculated.address}, , {calculated.finalPrice}");

    }
}
