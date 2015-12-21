using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using jqGrid01.Models;
using JqGridHelper.Models;
using System.Linq.Dynamic; //Import the Dynamic LINQ library

namespace jqGrid01.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetProducts(JqGridRequest request, string hiddenColumns)
        {
            var list = ProductDataSource.LatestProducts;

            var pageIndex = request.page - 1;
            var pageSize = request.rows;
            var totalRecords = list.Count;
            var totalPages = (int)Math.Ceiling(totalRecords / (float)pageSize);

            var products = list.AsQueryable()
                               .OrderBy(request.sidx + " " + request.sord)
                               .Skip(pageIndex * pageSize)
                               .Take(pageSize)
                               .ToList();

            var jqGridData = new JqGridData
            {
                UserData = new // نمایش در فوتر
                {
                    Name = "جمع صفحه",
                    Price = products.Sum(x => x.Price)
                },
                Total = totalPages,
                Page = request.page,
                Records = totalRecords,
                Rows = (products.Select(product => new JqGridRowData
                                                {
                                                    Id = product.Id,
                                                    RowCells = new List<object>
                                                    {
                                                        product.Id.ToString(CultureInfo.InvariantCulture),
                                                        product.Name,
                                                        product.IsAvailable.ToString(),
                                                        product.Price.ToString(CultureInfo.InvariantCulture)
                                                    }
                                                })).ToList()
            };
            return Json(jqGridData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult HiddenColumns(string[] hiddenColumns)
        {
            //todo: save in db or cookies or session ....

            return Content("OK");
        }
    }
}