using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCarts.Domain.Models
{
    public record UnvalidatedCustomerOrder(string OrderRegistrationCode, string OrderDescription, string OrderAmount, string OrderAddress, string OrderPrice)
    {

    }
}
