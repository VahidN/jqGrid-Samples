using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using jqGrid11.Models;
using System.Linq.Dynamic; //Import the Dynamic LINQ library
using JqGridHelper.Models;

namespace jqGrid11.Controllers
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

        public ActionResult GetGetSupplierData(int id, string name)
        {
            var list = ProductDataSource.LatestProducts;
            var products = list.Where(x => x.Id == id).ToList();
            if (!products.Any())
                return Json(null, JsonRequestBehavior.AllowGet);

            var productsData = new JqGridData
            {
                Rows = (products.Select(product => new JqGridRowData
                {
                    Id = product.Id,
                    RowCells = new List<object>
                               {
                                    product.Supplier.CompanyName,
                                    product.Supplier.Address,
                                    product.Supplier.PostalCode,
                                    product.Supplier.City,
                                    product.Supplier.Country,
                                    product.Supplier.Phone,
                                    product.Supplier.HomePage
                                }
                })).ToList()
            };
            return Json(productsData, JsonRequestBehavior.AllowGet);
        }
    }
}