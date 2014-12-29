using System;
using System.Collections.Generic;

namespace jqGrid12.Models
{
    /// <summary>
    /// منبع داده فرضی جهت سهولت دموی برنامه
    /// </summary>
    public static class OrdersDataSource
    {
        private static readonly IList<Order> _cachedItems;
        static OrdersDataSource()
        {
            _cachedItems = createOrdersDataSource();
        }

        public static IList<Order> LatestOrders
        {
            get { return _cachedItems; }
        }

        /// <summary>
        /// هدف صرفا تهیه یک منبع داده آزمایشی ساده تشکیل شده در حافظه است
        /// </summary>        
        private static IList<Order> createOrdersDataSource()
        {
            var list = new List<Order>();
            for (var i = 0; i < 100; i++)
            {
                var order = new Order
                {
                    Id = i + 1,
                    AddDateTime = DateTime.Now
                };

                var rnd = new Random();
                for (var j = 0; j < 30; j++)
                {
                    order.OrderDetails.Add(new OrderDetail
                    {
                        Id = j + 1,
                        Price = rnd.Next(1000, 2000),
                        Product = "محصول " + rnd.Next(1, 2000),
                        Unit = rnd.Next(1, 10)
                    });
                }

                list.Add(order);
            }
            return list;
        }
    }
}