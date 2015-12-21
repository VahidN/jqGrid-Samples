using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using jqGrid02.Models;
using System.Linq.Dynamic; //Import the Dynamic LINQ library
using JqGridHelper.Models;

namespace jqGrid02.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetProducts(JqGridRequest request)
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

            var productsData = new JqGridData
            {
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
                                                        product.Price.ToString(CultureInfo.InvariantCulture)
                                                    }
                                                })).ToList()
            };
            return Json(productsData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGetSupplierData(int id)
        {
            var list = ProductDataSource.LatestProducts;
            var product = list.FirstOrDefault(x => x.Id == id);
            if (product == null)
                return Json(null, JsonRequestBehavior.AllowGet);

            return Json(new
                          {
                              product.Supplier.CompanyName,
                              product.Supplier.Address,
                              product.Supplier.PostalCode,
                              product.Supplier.City,
                              product.Supplier.Country,
                              product.Supplier.Phone,
                              product.Supplier.HomePage
                          }, JsonRequestBehavior.AllowGet);
        }
    }
}