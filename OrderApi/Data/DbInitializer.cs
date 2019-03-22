using System.Collections.Generic;
using System.Linq;
using SharedModels;
using System;

namespace OrderApi.Data
{
    public class DbInitializer : IDbInitializer
    {
        // This method will create and seed the database.
        public void Initialize(OrderApiContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Look for any Products
            if (context.Orders.Any())
            {
                return;   // DB has been seeded
            }

            List<Order> orders = new List<Order>
            {
                new Order { Date = DateTime.Today, ProductId = 1, CustomerId = 2, Quantity = 2 },
                new Order { Date = DateTime.Today, ProductId = 2, CustomerId = 1, Quantity = 3 }
            };

            context.Orders.AddRange(orders);
            context.SaveChanges();
        }
    }
}
