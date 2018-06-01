using OrderBook.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBook.EF
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderBookDbContext _context = new OrderBookDbContext();

        #region sync implementation
        public ICollection<Order> GetAll()
        {
            try
            {
                ICollection<Order> orders = _context.Orders.ToList();
                return orders;
            }
            catch (SqlException ex)
            {
                ErrorMessage msg = ex.ToErrorMessage("Error occurred when get an order list");
                throw new ErrorDbException(msg);
            }
        }

        public ICollection<OrderDetail> GetOrderDetailsByNum(int orderNum)
        {
            //Order order = _context.Orders.Include(o => o.Details.Select(d => d.Product))
            //                             .FirstOrDefault(o => o.OrderNum == orderNum);
            //if (order != null)
            //    return order.Details;

            try
            {
                var details = _context.Database.SqlQuery<OrderDetailWithProduct>(
                                                             @"SELECT d.Id		    as Id
                                                                    , d.OrderId     as OrderId
                                                                    , d.ProductId   as ProductId
                                                                    , d.Quantity    as Quantity
                                                                    , p.Name        as ProductName
                                                                    , p.Price       as ProductPrice
                                                                 FROM order_detail d
                                                                 JOIN orders o ON o.Id = d.OrderId
                                                                 JOIN products p ON p.Id = d.ProductId
                                                                WHERE o.OrderNum = @num", new SqlParameter("@num", orderNum))
                                                .Select(x => new OrderDetail
                                                {
                                                    Id = x.Id,
                                                    OrderId = x.OrderId,
                                                    Quantity = x.Quantity,
                                                    ProductId = x.ProductId,
                                                    Product = new Product
                                                    {
                                                        Id = x.ProductId,
                                                        Name = x.ProductName,
                                                        Price = x.ProductPrice
                                                    }
                                                })
                                                .ToList();
                return details;
            }
            catch (SqlException ex)
            {
                ErrorMessage msg = ex.ToErrorMessage(string.Format("Error occurred when get an order details of order #{0}", orderNum));
                throw new ErrorDbException(msg);
            }
        }

        public ICollection<OrderDetailWithProduct> GetOrderDetailWithProductsByNum(int orderNum)
        {
            try
            {
                var details = _context.Database.SqlQuery<OrderDetailWithProduct>(
                                                             @"SELECT d.Id		    as Id
                                                                    , d.OrderId     as OrderId
                                                                    , d.ProductId   as ProductId
                                                                    , d.Quantity    as Quantity
                                                                    , p.Name        as ProductName
                                                                    , p.Price       as ProductPrice
                                                                 FROM order_detail d
                                                                 JOIN orders o ON o.Id = d.OrderId
                                                                 JOIN products p ON p.Id = d.ProductId
                                                                WHERE o.OrderNum = @num", new SqlParameter("@num", orderNum)).ToList();

                return details;
            }
            catch (SqlException ex)
            {
                ErrorMessage msg = ex.ToErrorMessage(string.Format("Error occurred when get an order details of order #{0}", orderNum));
                throw new ErrorDbException(msg);
            }
        }

        public Order SetComplete(int orderNum)
        {
            try
            {
                Order order = _context.Orders.FirstOrDefault(o => o.OrderNum == orderNum);

                if (order != null)
                {
                    order.Completed = DateTime.Now;
                    _context.Entry<Order>(order).State = EntityState.Modified;
                    _context.SaveChanges();
                }

                return order;
            }

            catch (SqlException ex)
            {
                ErrorMessage msg = ex.ToErrorMessage(string.Format("Error occurred when set completed order #{0}", orderNum));
                throw new ErrorDbException(msg);
            }
        }
        #endregion

        #region async implementation
        public async Task<ICollection<Order>> GetAllAsync()
        {
            try
            {
                var orders = await _context.Orders.ToListAsync();
                return orders;
            }
            catch (SqlException ex)
            {
                ErrorMessage msg = ex.ToErrorMessage("Error occurred when get an order list");
                throw new ErrorDbException(msg);
            }
        }

        public async Task<ICollection<OrderDetail>> GetOrderDetailsByNumAsync(int orderNum)
        {
            //Order order = await _context.Orders.Include(o => o.Details.Select(d => d.Product))
            //                                    .FirstOrDefaultAsync(o => o.OrderNum == orderNum);
            //if (order != null)
            //    return order.Details;

            try
            {
                var details = (await _context.Database.SqlQuery<OrderDetailWithProduct>(
                                            @"SELECT d.Id		   as Id
                                                   , d.OrderId     as OrderId
                                                   , d.ProductId   as ProductId
                                                   , d.Quantity    as Quantity
                                                   , p.Name        as ProductName
                                                   , p.Price       as ProductPrice
                                                FROM order_detail d
                                                JOIN orders o ON o.Id = d.OrderId
                                                JOIN products p ON p.Id = d.ProductId
                                               WHERE o.OrderNum = @num", new SqlParameter("@num", orderNum))
                                            .ToListAsync()
                           ).Select(x => new OrderDetail
                           {
                               Id = x.Id,
                               OrderId = x.OrderId,
                               Quantity = x.Quantity,
                               ProductId = x.ProductId,
                               Product = new Product
                               {
                                   Id = x.ProductId,
                                   Name = x.ProductName,
                                   Price = x.ProductPrice
                               }
                           }).ToList();

                return details;
            }
            catch (SqlException ex)
            {
                ErrorMessage msg = ex.ToErrorMessage(string.Format("Error occurred when get an order details of order #{0}", orderNum));
                throw new ErrorDbException(msg);
            }
        }

        public async Task<ICollection<OrderDetailWithProduct>> GetOrderDetailWithProductsByNumAsync(int orderNum)
        {
            try
            {
                var details = await _context.Database.SqlQuery<OrderDetailWithProduct>(
                                            @"SELECT d.Id		   as Id
                                                   , d.OrderId     as OrderId
                                                   , d.ProductId   as ProductId
                                                   , d.Quantity    as Quantity
                                                   , p.Name        as ProductName
                                                   , p.Price       as ProductPrice
                                                FROM order_detail d
                                                JOIN orders o ON o.Id = d.OrderId
                                                JOIN products p ON p.Id = d.ProductId
                                               WHERE o.OrderNum = @num", new SqlParameter("@num", orderNum))
                                            .ToListAsync();
                return details;
            }
            catch (SqlException ex)
            {
                ErrorMessage msg = ex.ToErrorMessage(string.Format("Error occurred when get an order details of order #{0}", orderNum));
                throw new ErrorDbException(msg);
            }
        }

        public async Task<Order> SetCompleteAsync(int orderNum)
        {
            try
            {
                Order order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderNum == orderNum);

                if (order != null)
                {
                    order.Completed = DateTime.Now;
                    _context.Entry<Order>(order).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }

                return order;
            }
            catch (SqlException ex)
            {
                ErrorMessage msg = ex.ToErrorMessage(string.Format("Error occurred when set completed order #{0}", orderNum));
                throw new ErrorDbException(msg);
            }
        }
        #endregion

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
