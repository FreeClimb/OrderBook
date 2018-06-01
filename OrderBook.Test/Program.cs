using OrderBook.Domain;
using OrderBook.EF;

namespace OrderBook.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            IOrderRepository repository = new OrderRepository();

            var orders = repository.GetAllAsync().Result;

            var details = repository.GetOrderDetailsByNumAsync(4).Result;

            Order order = repository.SetCompleteAsync(4).Result;
        }
    }
}
