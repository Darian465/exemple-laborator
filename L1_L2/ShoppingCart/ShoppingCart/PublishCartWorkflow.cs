using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShoppingCart.Domain.Cart;
using ShoppingCart.Domain;
using static ShoppingCart.CartPublishedEvent;
using static ShoppingCart.CartOperation;

namespace ShoppingCart
{
    public class PublishCartWorkflow
    {
        public ICartPublishedEvent Execute(PublishCodCommand command, Func<Cod, bool> checkCodExists)
        {
            GolCart golCart = new GolCart(command.InputProdus);
            ICart cart = ValidateCart(checkCodExists, golCart);
            cart = CalculateFinalCantitate(cart);
            cart = PublishCart(cart);

            return cart.Match(
                whenGolCart: golCart => new CartPublishFailEvent("Unexpected unvalidated state") as ICartPublishedEvent,
                whenNevalidatCart: nevalidatCart => new CartPublishFailEvent(nevalidatCart.Reason),
                whenValidareCart: validareCart => new CartPublishFailEvent("Unexpected validated state"),
                whenCalculareCart: calculareCart => new CartPublishFailEvent("Unexpected validated state"),
                whenPlatireCart: platireCart => new CartPublishSucceededEvent(platireCart.Csv,platireCart.PublishedDate)
            );
        }
    }
}
