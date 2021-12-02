using System;
using CSharp.Choices;
using System.Collections.Generic;


namespace ShoppingCarts.Domain.Models
{
    [AsChoice]
    public static partial class PlacingOrderEvent
    {
        public interface IPlacingOrderEvent { }
        public record PlacingOrderEventSuccedeedEvent : IPlacingOrderEvent
        {
            public decimal NumberOfOrder { get; }
            public DateTime PlacedDate { get; }

            public string Csv { get; }
            public IReadOnlyCollection<CalculateCustomerOrder> CalculatedOrder { get; }
            internal PlacingOrderEventSuccedeedEvent(IReadOnlyCollection<CalculateCustomerOrder> calculatedOrder, string csv, decimal numberOfOrder, DateTime placedDate)
            {
                CalculatedOrder = calculatedOrder;
                Csv = csv;
                NumberOfOrder = numberOfOrder;
                PlacedDate = placedDate;
            }
        }
        public record PlacingOrderEventFailedEvent : IPlacingOrderEvent
        {
            public string Reason { get; }
            internal PlacingOrderEventFailedEvent(string reason)
            {
                Reason = reason;
            }
        }
    }
}
