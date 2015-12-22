using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using jqGrid03.Models;
using JqGridHelper.DynamicSearch;
using JqGridHelper.Models;
using System.Linq.Dynamic; //Import the Dynamic LINQ library

namespace jqGrid03.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetProducts(JqGridRequest request)
        {
            var list = ProductDataSource.LatestProducts;

            var pageIndex = request.page - 1;
            var pageSize = request.rows;
            var totalRecords = list.Count;
            var totalPages = (int)Math.Ceiling(totalRecords / (float)pageSize);

            var productsQuery = list.AsQueryable();
            //System.Collections.Generic.List`1[jqGrid03.Models.Product].Where(Param_0 => Param_0.Code.Value.ToString().Equals(null))}
            productsQuery = new JqGridSearch(request, this.Request.Form, DateTimeType.Persian).ApplyFilter(productsQuery);
            var productsList = productsQuery.OrderBy(request.sidx + " " + request.sord)
                                            .Skip(pageIndex * pageSize)
                                            .Take(pageSize)
                                            .ToList();

            var productsData = new JqGridData
            {
                Total = totalPages,
                Page = request.page,
                Records = totalRecords,
                Rows = (productsList.Select(product => new JqGridRowData
                {
                    Id = product.Id,
                    RowCells = new List<object>
                               {
                                     product.Id.ToString(CultureInfo.InvariantCulture),
                                     product.Name,
                                     product.Supplier.CompanyName,
                                     product.Category.Name,
                                     product.Price.ToString(CultureInfo.InvariantCulture),
                                     product.Code.HasValue? product.Code.Value.ToString("D") : string.Empty
                                }
                })).ToArray()
            };
            return Json(productsData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SuppliersSelect()
        {
            var list = ProductDataSource.LatestProducts;
            var suppliers = list.Select(x => new SelectListItem
            {
                Text = x.Supplier.CompanyName,
                Value = x.Supplier.Id.ToString(CultureInfo.InvariantCulture)
            }).ToList();
            return PartialView("_SelectPartial", suppliers);
        }

        public ActionResult CategoriesSelect()
        {
            var list = ProductDataSource.LatestProducts;
            var categories = list.Select(x => new SelectListItem
            {
                Text = x.Category.Name,
                Value = x.Category.Id.ToString(CultureInfo.InvariantCulture)
            }).ToList();
            return PartialView("_SelectPartial", categories);
        }
    }
}