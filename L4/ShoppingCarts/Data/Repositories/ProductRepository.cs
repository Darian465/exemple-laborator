﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingCarts.Domain.Models;
using ShoppingCarts.Domain.Repositories;
using LanguageExt;

namespace ShoppingCarts.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly OrdersContext orderContext;

        public ProductRepository(OrdersContext orderContext)
        {
            this.orderContext = orderContext;
        }

        public TryAsync<List<OrderRegistrationCode>> TryGetExistingOrders(IEnumerable<string> ordersToCheck) => async () =>
        {
            var orders = await orderContext.Products
                                              .Where(order => ordersToCheck.Contains(order.RegistrationCode))
                                              .AsNoTracking()
                                              .ToListAsync();

            return orders.Select(order => new OrderRegistrationCode(order.RegistrationCode))
                           .ToList();
        };
    }
}
