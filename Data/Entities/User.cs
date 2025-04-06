using System;

namespace imsapi.Data.Entities
{
    public class User: EntityBase
    {
        public string fullName { get; set; }= "New User";
        public string? email { get; set; }
        public string? phone { get; set; }
        public string passwordHash { get; set; }
        public string? image { get; set; }
        public int storeId { get; set; }
        public ERole role { get; set; }= ERole.Seller;
        public virtual Store? Store { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public ICollection<Payment> Payments { get; set; }
        
    }
}