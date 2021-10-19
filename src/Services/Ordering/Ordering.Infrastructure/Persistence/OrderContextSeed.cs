using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async void SeedOrders(OrderContext ordersContext, ILogger<OrderContextSeed> logger)
        {
            if(ordersContext.Orders.Any())
            {
                ordersContext.Orders.AddRange(GetPreconfiguredOrders());
                await ordersContext.SaveChangesAsync();
                logger.LogInformation("Orders Database was seeded with context {OrderContext}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() {UserName = "swn", FirstName = "Mehmet", LastName = "Ozkaya", EmailAddress = "ezozkme@gmail.com", AddressLine = "Bahcelievler", Country = "Turkey", TotalPrice = 350 }
            };
        }
    }
}
