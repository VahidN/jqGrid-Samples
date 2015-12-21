using System;

namespace jqGrid03.Models
{
    public class Product
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public decimal Price { set; get; }
        public Supplier Supplier { set; get; }
        public Category Category { set; get; }
        public Guid Code { set; get; }
    }

    public class Category
    {
        public int Id { set; get; }
        public string Name { set; get; }
    }

    public class Supplier
    {
        public int Id { set; get; }
        public string CompanyName { set; get; }
        public string Address { set; get; }
        public string PostalCode { set; get; }
        public string City { set; get; }
        public string Country { set; get; }
        public string Phone { set; get; }
        public string HomePage { set; get; }
    }
}