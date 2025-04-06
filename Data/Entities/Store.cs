using System;

namespace imsapi.Data.Entities
{
    public class Store: EntityBase
    {
        public string name { get; set; }= "New Store";
        public string? email { get; set; }
        public string? address { get; set; }
        public string? phone { get; set; }
        public string? image { get; set; }
        public string? description { get; set; }
        public bool blocked { get; set; }=false;
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }

    }
}