using System;

namespace imsapi.Data.Entities
{
    public class Product: EntityBase
    {
        public string name { get; set; }
        public string nameLower { get; set; }
        public string? description { get; set; }
        public string? image { get; set; }
        public decimal purchasePrice { get; set; }
        public decimal salePrice { get; set; }
        public int quantity { get; set; }
        public int storeId { get; set; }
         public string? unit  { get; set; }
        public virtual Store? Store { get; set; }
        public int categoryId { get; set; }
        public int userId { get; set; }
        public virtual User? User { get; set; }
        public virtual Category? Category { get; set; }
        public ICollection<PaymentItem> PaymentItems { get; set; }
    }
}