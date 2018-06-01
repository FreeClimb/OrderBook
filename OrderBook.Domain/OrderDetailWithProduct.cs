using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBook.Domain
{
    public class OrderDetailWithProduct
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public float ProductPrice { get; set; }
        public float Total
        {
            get
            {
                    return (this.ProductPrice * this.Quantity);
            }
        }
    }
}
