using System;
using System.Collections.Generic;
using System.Linq;

namespace jqGrid12.Models
{
    public class Order
    {
        public int Id { set; get; }
        public DateTime AddDateTime { set; get; }

        public int ItemsCount
        {
            get { return OrderDetails.Count(); }
        }

        public int TotalPrice
        {
            get { return OrderDetails.Sum(x => x.Price); }
        }

        public IList<OrderDetail> OrderDetails { set; get; }

        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }
    }
}