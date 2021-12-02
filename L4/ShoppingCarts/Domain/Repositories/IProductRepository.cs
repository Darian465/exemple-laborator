using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCarts.Domain.Models;
using LanguageExt;

namespace ShoppingCarts.Domain.Repositories
{
    public interface IProductRepository
    {
        TryAsync<List<OrderRegistrationCode>> TryGetExistingOrders(IEnumerable<string> ordersToCheck);
    }
}
