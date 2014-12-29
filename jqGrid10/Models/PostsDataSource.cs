using System;
using System.Collections.Generic;

namespace jqGrid10.Models
{
    /// <summary>
    /// منبع داده فرضی جهت سهولت دموی برنامه
    /// </summary>
    public static class PostsDataSource
    {
        private static readonly IList<Post> _cachedItems;
        static PostsDataSource()
        {
            _cachedItems = createPostsDataSource();
        }

        public static IList<Post> LatestPosts
        {
            get { return _cachedItems; }
        }

        /// <summary>
        /// هدف صرفا تهیه یک منبع داده آزمایشی ساده تشکیل شده در حافظه است
        /// </summary>        
        private static IList<Post> createPostsDataSource()
        {
            var list = new List<Post>();
            var rnd = new Random();
            for (var i = 0; i < 50; i++)
            {
                list.Add(new Post
                {
                    Id = i + 1,
                    Title = "عنوان " + (i + 1),
                    CategoryName = "گروه " + rnd.Next(1, 10),
                    NumberOfViews = rnd.Next(100, 1000)
                });
            }
            return list;
        }
    }
}