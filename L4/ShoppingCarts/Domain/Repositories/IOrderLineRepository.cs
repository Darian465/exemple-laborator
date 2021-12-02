using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCarts.Domain.Models;
using LanguageExt;
using static ShoppingCarts.Domain.Models.OrdersCart;

namespace ShoppingCarts.Domain.Repositories
{
    public interface IOrderLineRepository
    {
        TryAsync<List<CalculateCustomerOrder>> TryGetExistingOrders();

        TryAsync<Unit> TrySaveOrders(PlacedOrder order);
    }
}
