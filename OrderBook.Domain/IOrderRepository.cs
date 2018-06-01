using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderBook.Domain
{
    public interface IOrderRepository : IDisposable
    {
        #region sync
        ICollection<Order> GetAll();

        ICollection<OrderDetail> GetOrderDetailsByNum(int orderNum);

        ICollection<OrderDetailWithProduct> GetOrderDetailWithProductsByNum(int orderNum);

        Order SetComplete(int orderNum);
        #endregion

        #region async
        Task<ICollection<Order>> GetAllAsync();

        Task<ICollection<OrderDetail>> GetOrderDetailsByNumAsync(int orderNum);

        Task<ICollection<OrderDetailWithProduct>> GetOrderDetailWithProductsByNumAsync(int orderNum);

        Task<Order> SetCompleteAsync(int orderNum);

        #endregion
    }
}
