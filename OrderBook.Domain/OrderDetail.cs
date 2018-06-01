using System;

namespace OrderBook.Domain
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public float Total
        {
            get
            {
                if (Product != null)
                {
                    return (this.Product.Price * this.Quantity);
                }
                return 0;
            }
        }
    }
}
