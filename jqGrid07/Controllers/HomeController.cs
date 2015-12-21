using jqGrid07.Models;
using System.Linq.Dynamic; //Import the Dynamic LINQ library
using JqGridHelper.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jqGrid07.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<Product> _productsInMemoryDataSource = new List<Product>();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetProducts(JqGridRequest request)
        {
            var list = _productsInMemoryDataSource;

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
                                     product.ImageName
                               }
                })).ToArray()
            };
            return Json(productsData, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult DeleteProduct(string id)
        {
            //todo: Delete product
            var product = _productsInMemoryDataSource.FirstOrDefault(x => x.Id == int.Parse(id));
            if (product == null)
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);

            _productsInMemoryDataSource.Remove(product);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddProduct(Product postData)
        {
            //todo: Add product to repository
            if (postData == null)
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);

            postData.Id = _productsInMemoryDataSource.LastOrDefault() == null ? 1 : _productsInMemoryDataSource.Last().Id + 1;
            _productsInMemoryDataSource.Add(postData);

            return Json(new { id = postData.Id, success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditProduct(Product postData)
        {
            var product = _productsInMemoryDataSource.FirstOrDefault(x => x.Id == postData.Id);
            if (product == null)
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);

            product.Name = postData.Name;

            //todo: save changes

            return Json(new { id = postData.Id, success = true }, JsonRequestBehavior.AllowGet);
        }


        // todo: change `imageName` according to the form's file element name
        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase imageName, int id)
        {
            if (imageName == null)
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);

            var product = _productsInMemoryDataSource.FirstOrDefault(x => x.Id == id);
            if (product == null)
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);

            // update record
            product.ImageName = Path.GetFileName(imageName.FileName);

            // save image
            var filePath = Path.Combine(Server.MapPath("~/images"), product.ImageName);
            imageName.SaveAs(filePath);

            //todo: save changes

            return Json(new { FileName = product.ImageName }, "text/html", JsonRequestBehavior.AllowGet);
        }
    }
}