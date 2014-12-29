using System;
using System.Collections.Generic;

namespace jqGrid05.Models
{
    /// <summary>
    /// منبع داده فرضی جهت سهولت دموی برنامه
    /// </summary>
    public static class ProductDataSource
    {
        private static readonly IList<Product> _cachedItems;
        static ProductDataSource()
        {
            _cachedItems = createProductsDataSource();
        }

        public static IList<Product> LatestProducts
        {
            get { return _cachedItems; }
        }

        /// <summary>
        /// هدف صرفا تهیه یک منبع داده آزمایشی ساده تشکیل شده در حافظه است
        /// </summary>        
        private static IList<Product> createProductsDataSource()
        {
            var list = new List<Product>();
            for (var i = 0; i < 500; i++)
            {
                list.Add(new Product
                {
                    Id = i + 1,
                    Name = "نام " + (i + 1),
                    Price = i * 1000,
                    AddDate = DateTime.Now.AddDays(-i)
                });
            }
            return list;
        }
    }
}