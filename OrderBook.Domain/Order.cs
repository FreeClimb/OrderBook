using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderBook.Domain
{
    public class Order
    {
        public Order()
        {
            this.Details = new List<OrderDetail>();
        }

        public int Id { get; set; }

        public int OrderNum { get; set; }

        public string Customer { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Completed { get; set; }

        public ICollection<OrderDetail> Details { get; set; }

        public string Status
        {
            get
            {
                return this.Completed.HasValue ? "Complete" : "In progress";
            }
        }

        public string OrderName
        {
            get
            {
                return string.Format("Order #{0}", this.OrderNum);
            }
        }

        public float Total
        {
            get
            {
                if (this.Details.Any())
                {
                    return Details.Sum(d => d.Total);
                }

                return 0;
            }
        } 
    }
}
