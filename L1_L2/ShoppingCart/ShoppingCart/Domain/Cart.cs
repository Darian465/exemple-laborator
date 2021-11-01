using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain
{
    [AsChoice]
    public static partial class Cart
    {
        public interface ICart { }
        public record GolCart : ICart
        {
            public GolCart(IReadOnlyCollection<UnvalidatedProdus> produsList)
            {
                ProdusList = produsList;
            }
            public IReadOnlyCollection<UnvalidatedProdus> ProdusList { get; }
        }
            
        public record NevalidatCart : ICart
        {
            internal NevalidatCart(IReadOnlyCollection<UnvalidatedProdus> produsList, string reason)
            {
                ProdusList = produsList;
                Reason = reason;
            }

            public IReadOnlyCollection<UnvalidatedProdus> ProdusList { get; }
            public string Reason { get; }
        }
        public record ValidareCart : ICart
        {
           internal ValidareCart (IReadOnlyCollection<ValidatedProdus> produsList)
            {
                ProdusList = produsList;
            }
            public IReadOnlyCollection<ValidatedProdus> ProdusList { get; }
        }
        public record CalculareCart : ICart
        {
            internal CalculareCart(IReadOnlyCollection<CalculatedProdus> produsList)
            {
                ProdusList = produsList;
            }
            public IReadOnlyCollection<CalculatedProdus> ProdusList { get; }
        }
        public record PlatireCart : ICart
        {
           internal PlatireCart(IReadOnlyCollection<CalculatedProdus> produsList, string csv, DateTime publishedDate)
            {
                ProdusList = produsList;
                PublishedDate = publishedDate;
                Csv = csv;
            }
            public IReadOnlyCollection<CalculatedProdus> ProdusList { get; }
            public DateTime PublishedDate { get; }
            public string Csv { get; }
        }
    }
}
