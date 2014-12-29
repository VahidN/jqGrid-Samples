using System;

namespace jqGrid13.Models
{
    public class BlogComment
    {
        // Other properties 
        public int Id { set; get; }
        public string Body { set; get; }
        public DateTime AddDateTime { set; get; }

        // for treeGridModel: 'adjacency'
        public int? ParentId { get; set; }
        public bool IsNotExpandable { get; set; }
        public bool IsExpanded { get; set; }
    }
}