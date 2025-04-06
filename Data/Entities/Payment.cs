using System;

namespace imsapi.Data.Entities
{
    public class Payment: EntityBase
    {
        public int Id { get; set; }
        public decimal amount { get; set; }
        public string paymentMethod { get; set; }
        public int storeId { get; set; }
        public virtual Store Store { get; set; }
        public int userId { get; set; }
        public virtual User User { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<PaymentItem> PaymentItems { get; set; }
        public Customer Customer { get; set; }

        public int customerId { get; set; }
        
    }
}