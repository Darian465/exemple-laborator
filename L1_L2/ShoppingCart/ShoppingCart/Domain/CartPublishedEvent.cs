using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp.Choices;

namespace ShoppingCart
{
    [AsChoice]
    public static partial class CartPublishedEvent
    {
        public interface ICartPublishedEvent { }
        public record CartPublishSucceededEvent : ICartPublishedEvent
        {
            public string Csv { get; }
            public DateTime PublishedDate { get; }
            internal CartPublishSucceededEvent(string csv, DateTime publishedDate)
            {
                Csv = csv;
                PublishedDate = publishedDate;
            }
        }
        public record CartPublishFailEvent : ICartPublishedEvent
        {
            public string Reason { get; }
            internal CartPublishFailEvent(string reason)
            {
                Reason = reason;
            }
        }
    }
}
