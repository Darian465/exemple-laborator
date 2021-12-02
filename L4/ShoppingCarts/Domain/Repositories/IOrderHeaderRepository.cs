using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCarts.Domain.Models;
using LanguageExt;

namespace ShoppingCarts.Domain.Repositories
{
    public interface IOrderHeaderRepository
    {
        TryAsync<List<int>> TryGetExistingOrders(IEnumerable<int> ordertsToCheck);
    }
}
