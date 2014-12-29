using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using jqGrid09.Models;
using JqGridHelper.Models;
using JqGridHelper.DynamicSearch;

namespace jqGrid09.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<User> _usersInMemoryDataSource = new List<User>();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetUsers(JqGridRequest request)
        {
            var list = _usersInMemoryDataSource;

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
                Rows = (productsList.Select(user => new JqGridRowData
                {
                    Id = user.Id,
                    RowCells = new List<object>
                               {
                                     user.Id.ToString(CultureInfo.InvariantCulture),
                                     user.Name,
                                     user.Email,
                                     "", // عدم نمایش کلمه عبور در گرید
                                     user.SiteUrl
                               }
                })).ToArray()
            };
            return Json(productsData, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult DeleteUser(string id)
        {
            //todo: Delete user
            var user = _usersInMemoryDataSource.FirstOrDefault(x => x.Id == int.Parse(id));
            if (user == null)
            {
                this.Response.StatusCode = 500; //این مورد برای افزودن داخل ردیف‌های گرید لازم است
                return Json(new {success = false, message = "کاربر یافت نشد"}, JsonRequestBehavior.AllowGet);
            }

            _usersInMemoryDataSource.Remove(user);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddUser(User postData)
        {
            //todo: Add user to repository
            if (postData == null)
            {
                this.Response.StatusCode = 500; //این مورد برای افزودن داخل ردیف‌های گرید لازم است
                return Json(new {success = false, message = "اطلاعات دریافتی خالی است"}, JsonRequestBehavior.AllowGet);
            }

            if (_usersInMemoryDataSource.Any(
                    user => user.Name.Equals(postData.Name, StringComparison.InvariantCultureIgnoreCase)))
            {
                this.Response.StatusCode = 500; //این مورد برای افزودن داخل ردیف‌های گرید لازم است
                return Json(new { success = false, message = "نام کاربر تکراری است" }, JsonRequestBehavior.AllowGet);
            }

            if (_usersInMemoryDataSource.Any(
                    user => user.Email.Equals(postData.Email, StringComparison.InvariantCultureIgnoreCase)))
            {
                this.Response.StatusCode = 500; //این مورد برای افزودن داخل ردیف‌های گرید لازم است
                return Json(new { success = false, message = "آدرس ایمیل کاربر تکراری است" }, JsonRequestBehavior.AllowGet);
            }

            postData.Id = _usersInMemoryDataSource.LastOrDefault() == null ? 1 : _usersInMemoryDataSource.Last().Id + 1;
            _usersInMemoryDataSource.Add(postData);

            return Json(new { id = postData.Id, success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditUser(User postData, string oper)
        {
            if (oper == "add")
            {
                return AddUser(postData);
            }

            var dbUser = _usersInMemoryDataSource.FirstOrDefault(x => x.Id == postData.Id);
            if (dbUser == null)
            {
                this.Response.StatusCode = 500; //این مورد برای افزودن داخل ردیف‌های گرید لازم است
                return Json(new {success = false, message = "کاربر یافت نشد"}, JsonRequestBehavior.AllowGet);
            }

            if (_usersInMemoryDataSource.Any(
                    user => user.Id != postData.Id &&
                            user.Name.Equals(postData.Name, StringComparison.InvariantCultureIgnoreCase)))
            {
                this.Response.StatusCode = 500; //این مورد برای افزودن داخل ردیف‌های گرید لازم است
                return Json(new { success = false, message = "نام کاربر تکراری است" }, JsonRequestBehavior.AllowGet);
            }

            if (_usersInMemoryDataSource.Any(
                    user => user.Id != postData.Id &&
                            user.Email.Equals(postData.Email, StringComparison.InvariantCultureIgnoreCase)))
            {
                this.Response.StatusCode = 500; //این مورد برای افزودن داخل ردیف‌های گرید لازم است
                return Json(new { success = false, message = "آدرس ایمیل کاربر تکراری است" }, JsonRequestBehavior.AllowGet);
            }

            dbUser.Name = postData.Name;
            dbUser.Email = postData.Email;
            dbUser.SiteUrl = postData.SiteUrl;

            if (!string.IsNullOrWhiteSpace(postData.Password))
            {
                dbUser.Password = postData.Password;
            }

            //todo: save changes

            return Json(new { id = postData.Id, success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}