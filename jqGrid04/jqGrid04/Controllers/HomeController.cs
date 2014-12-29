using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using jqGrid04.Models;
using JqGridHelper.DynamicSearch;
using JqGridHelper.Models;

namespace jqGrid04.Controllers
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
                                     product.Price.ToString(CultureInfo.InvariantCulture)
                                }
                })).ToList()
            };
            return Json(productsData, JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult DeleteProduct(string id)
        {
            //todo: Delete product
            var product = ProductDataSource.LatestProducts.FirstOrDefault(x => x.Id == int.Parse(id));
            if (product == null)
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);

            ProductDataSource.LatestProducts.Remove(product);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddProduct(Product postData)
        {
            //todo: Add product to repository
            if (postData == null)
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);

            ProductDataSource.LatestProducts.Add(postData);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditProduct(Product postData)
        {
            //todo: Edit product based on postData

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
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