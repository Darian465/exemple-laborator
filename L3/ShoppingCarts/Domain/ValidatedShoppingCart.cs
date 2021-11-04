namespace ShoppingCarts.Domain
{
    public record ValidatedShoppingCart(ProductCode productCode, Quantity quantity, Address address, Price price);
}