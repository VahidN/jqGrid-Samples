using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using jqGrid10.Models;
using JqGridHelper.Models;
using System.Linq.Dynamic; //Import the Dynamic LINQ library

namespace jqGrid10.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetPosts(JqGridRequest request)
        {
            var list = PostsDataSource.LatestPosts;

            var pageIndex = request.page - 1;
            var pageSize = request.rows;
            var totalRecords = list.Count;
            var totalPages = (int)Math.Ceiling(totalRecords / (float)pageSize);

            var productsQuery = list.AsQueryable();

            var productsList = productsQuery.OrderBy(request.sidx + " " + request.sord)
                                            .Skip(pageIndex * pageSize)
                                            .Take(pageSize)
                                            .ToList();

            var productsData = new JqGridData
            {
                Total = totalPages,
                Page = request.page,
                Records = totalRecords,
                Rows = (productsList.Select(post => new JqGridRowData
                {
                    Id = post.Id,
                    RowCells = new List<object>
                               {
                                  post.Id.ToString(CultureInfo.InvariantCulture),
                                  post.Title,
                                  post.CategoryName,
                                  post.NumberOfViews.ToString(CultureInfo.InvariantCulture)
                               }
                })).ToArray()
            };
            return Json(productsData, JsonRequestBehavior.AllowGet);
        }
    }
}