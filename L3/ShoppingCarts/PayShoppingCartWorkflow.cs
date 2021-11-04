using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShoppingCarts.Domain.ShoppingCartsPaidEvent;
using static ShoppingCarts.Domain.ShoppingCarts;
using static ShoppingCarts.ShoppingCartsOperations;
using ShoppingCarts.Domain;
using LanguageExt;

namespace ShoppingCarts
{
    public class PayShoppingCartWorkflow
    {
        public async Task<IShoppingCartsPaidEvent> ExecuteAsync(PayShoppingCartCommand command, Func<ProductCode, TryAsync<bool>> checkProductExists)
        {
            EmptyShoppingCarts emptyShoppingCarts = new EmptyShoppingCarts(command.InputShoppingCarts);
            IShoppingCarts shoppingCarts =await ValidateShoppingCarts(checkProductExists, emptyShoppingCarts);
            shoppingCarts = CalculateFinalPrice(shoppingCarts);
            shoppingCarts = PayShoppingCart(shoppingCarts);

            return shoppingCarts.Match(
                    whenEmptyShoppingCarts: emptyShoppingCarts => new ShoppingCartsPaidFailedEvent("Unexpected unvalidated state") as IShoppingCartsPaidEvent,
                    whenUnvalidatedShoppingCarts: unvalidatedShoppingCarts => new ShoppingCartsPaidFailedEvent(unvalidatedShoppingCarts.Reason),
                    whenValidatedShoppingCarts: validatedShoppingCarts => new ShoppingCartsPaidFailedEvent("Unexpected validated state"),
                    whenCalculatedShoppingCarts: calculatedShoppingCarts => new ShoppingCartsPaidFailedEvent("Unexpected calculated state"),
                    whenPaidShoppingCarts: paidShoppingCarts => new ShoppingCartsPaidScucceededEvent(paidShoppingCarts.Csv, paidShoppingCarts.PublishedDate)
                );
        }
    }
}
