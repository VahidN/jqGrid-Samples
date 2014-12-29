using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using jqGrid13.Models;
using JqGridHelper.DynamicSearch; // for dynamic OrderBy
using JqGridHelper.Models;
using JqGridHelper.Utils;

namespace jqGrid13.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetComments(JqGridRequest request, int? nodeid, int? parentid, int? n_level)
        {
            var list = BlogCommentsDataSource.LatestBlogComments;

            // در این حالت خاص فعلا در نگارش جای جی‌کیو‌گرید صفحه بندی کار نمی‌کند و فعال نیست و محاسبات ذیل اهمیتی ندارند
            var pageIndex = request.page - 1;
            var pageSize = request.rows;
            var totalRecords = list.Count;
            var totalPages = (int)Math.Ceiling(totalRecords / (float)pageSize);

            var productsQuery = list.AsQueryable();
            
            if (nodeid == null)
            {
                productsQuery = productsQuery.Where(x => x.ParentId == null);
            }
            else
            {
                productsQuery = productsQuery.Where(x => x.ParentId == nodeid.Value);
            }

            var products = productsQuery.OrderBy(request.sidx + " " + request.sord)
                                        .Skip(pageIndex * pageSize)
                                        .Take(pageSize)
                                        .ToList();


            var newLevel = n_level == null ? 0 : n_level.Value + 1;
            var productsData = new JqGridData
            {
                Total = totalPages,
                Page = request.page,
                Records = totalRecords,
                Rows = (products.Select(comment => new JqGridRowData
                {
                    Id = comment.Id,
                    RowCells = new List<object> 
                               {
                                   comment.Id,
                                   comment.Body,
                                   comment.AddDateTime.ToPersianDate(),
                                   // اطلاعات خاص نمایش درختی به ترتیب
	                               newLevel,
	                               comment.ParentId == null ? "" : comment.ParentId.Value.ToString(CultureInfo.InvariantCulture),
	                               comment.IsNotExpandable,
	                               comment.IsExpanded
                               }
                })).ToList()
            };
            return Json(productsData, JsonRequestBehavior.AllowGet);
        }
    }
}