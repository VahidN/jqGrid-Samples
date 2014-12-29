using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using jqGrid12.Models;
using JqGridHelper.DynamicSearch; // for dynamic OrderBy
using JqGridHelper.Models;
using JqGridHelper.Utils;

namespace jqGrid12.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetOrders(JqGridRequest request)
        {
            var list = OrdersDataSource.LatestOrders;

            var pageIndex = request.page - 1;
            var pageSize = request.rows;
            var totalRecords = list.Count;
            var totalPages = (int)Math.Ceiling(totalRecords / (float)pageSize);

            var products = list.AsQueryable()
                               .OrderBy(request.sidx + " " + request.sord)
                               .Skip(pageIndex * pageSize)
                               .Take(pageSize)
                               .ToList();

            var productsData = new JqGridData
            {
                Total = totalPages,
                Page = request.page,
                Records = totalRecords,
                Rows = (products.Select(order => new JqGridRowData
                {
                    Id = order.Id,
                    RowCells = new List<object> 
                                                    {
                                                        order.Id.ToString(CultureInfo.InvariantCulture),
                                                        order.AddDateTime.ToPersianDate(),
                                                        order.ItemsCount.ToString(CultureInfo.InvariantCulture),
                                                        order.TotalPrice.ToString(CultureInfo.InvariantCulture)
                                                    }
                })).ToList()
            };
            return Json(productsData, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetOrderDetails(int id, JqGridRequest request)
        {
            var order = OrdersDataSource.LatestOrders.FirstOrDefault(x => x.Id == id);
            if (order == null)
                return new HttpNotFoundResult();

            var list = order.OrderDetails;

            var pageIndex = request.page - 1;
            var pageSize = request.rows;
            var totalRecords = list.Count;
            var totalPages = (int)Math.Ceiling(totalRecords / (float)pageSize);

            var orderDetails = list.AsQueryable()
                               .OrderBy(request.sidx + " " + request.sord)
                               .Skip(pageIndex * pageSize)
                               .Take(pageSize)
                               .ToList();

            var detailsData = new JqGridData
            {
                Total = totalPages,
                Page = request.page,
                Records = totalRecords,
                Rows = (orderDetails.Select(detail => new JqGridRowData
                {
                    Id = detail.Id,
                    RowCells = new List<object> 
                                                    {
                                                        detail.Id.ToString(CultureInfo.InvariantCulture),
                                                        detail.Product,
                                                        detail.Unit.ToString(CultureInfo.InvariantCulture),
                                                        detail.Price.ToString(CultureInfo.InvariantCulture)
                                                    }
                })).ToList()
            };
            return Json(detailsData, JsonRequestBehavior.AllowGet);
        }
    }
}