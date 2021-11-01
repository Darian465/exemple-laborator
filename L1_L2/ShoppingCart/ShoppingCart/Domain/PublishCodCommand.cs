using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShoppingCart.Domain.Cart;
namespace ShoppingCart.Domain
{
    public record PublishCodCommand
    {
        public PublishCodCommand(IReadOnlyCollection<UnvalidatedProdus> inputProdus)
        {
            InputProdus = inputProdus;
        }
        public IReadOnlyCollection<UnvalidatedProdus> InputProdus { get; }
    }
}
