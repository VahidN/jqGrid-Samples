using System;
using System.Collections.Generic;

namespace jqGrid13.Models
{
    /// <summary>
    /// منبع داده فرضی جهت سهولت دموی برنامه
    /// </summary>
    public static class BlogCommentsDataSource
    {
        private static readonly IList<BlogComment> _cachedItems;
        static BlogCommentsDataSource()
        {
            _cachedItems = createBlogCommentsDataSource();
        }

        public static IList<BlogComment> LatestBlogComments
        {
            get { return _cachedItems; }
        }

        /// <summary>
        /// هدف صرفا تهیه یک منبع داده آزمایشی ساده تشکیل شده در حافظه است
        /// </summary>        
        private static IList<BlogComment> createBlogCommentsDataSource()
        {
            var list = new List<BlogComment>();

            int id = 1;
            for (var i = 0; i < 20; i++)
            {
                var blogComment = new BlogComment
                {
                    Id = id,
                    AddDateTime = DateTime.Now,
                    Body = "نظر ریشه " + id,
                    ParentId = null, //نظرات اصلی
                    IsNotExpandable = false,
                    IsExpanded = false
                };
                list.Add(blogComment);
                id++;

                for (var j = 0; j < 5; j++)
                {
                    list.Add(new BlogComment
                    {
                        Id = id,
                        AddDateTime = DateTime.Now,
                        Body = "پاسخ " + (j + 1) + " به ریشه " + blogComment.Id,
                        ParentId = blogComment.Id, // پاسخ به نظرات ریشه اصلی
                        IsNotExpandable = false,
                        IsExpanded = false
                    });
                    id++;

                    list.Add(new BlogComment
                    {
                        Id = id,
                        AddDateTime = DateTime.Now,
                        Body = "پاسخ " + 1 + " به پاسخ " + (id - 1),
                        ParentId = id - 1, // پاسخ به نظرات داده شده در یک سطح دیگر
                        IsNotExpandable = true,
                        IsExpanded = false
                    });
                    id++;
                }
            }

            return list;
        }
    }
}