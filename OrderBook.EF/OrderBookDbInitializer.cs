using OrderBook.Domain;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;

namespace OrderBook.EF
{
    public class OrderBookDbInitializer : DropCreateDatabaseAlways<OrderBookDbContext>
    {
        protected override void Seed(OrderBookDbContext context)
        {
            IList<Product> products = new List<Product>();

            Random rnd = new Random();

            for (int i = 1; i <= 30; i++)
            {
                products.Add(new Product() { Name = string.Format("Product #{0}", i), Price = rnd.Next(i, 100) });
            }

            context.Products.AddRange(products);

            IList<Order> orders = new List<Order>();
            Order order;
            Product product;

            for (int i = 1; i <= 5; i++)
            {
                order = new Order()
                {
                    OrderNum = i,
                    Customer = i < 4 ? "Steve" : "Bob",
                    Created = DateTime.Now.AddDays(6 - i)
                };

                for (int j = 1; j < 10; j++)
                {
                    int k = rnd.Next(1, 30);
                    product = products.FirstOrDefault(p => p.Name.Equals(string.Format("Product #{0}", k)));

                    if (product != null)
                    {
                        order.Details.Add(new OrderDetail()
                        {
                            Product = product,
                            Quantity = rnd.Next(1, 10)
                        });
                    }
                }

                orders.Add(order);
            }

            context.Orders.AddRange(orders);

            base.Seed(context);
        }
    }
}
