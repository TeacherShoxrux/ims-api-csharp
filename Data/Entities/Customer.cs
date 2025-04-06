using System;

namespace imsapi.Data.Entities
{
    public class Customer: EntityBase
    {
        public int storeId { get; set; }
        public virtual Store Store { get; set; }
        public string fullName { get; set; }
        public string info { get; set; }
        public string phone { get; set; }
        public int userId { get; set; }
        public virtual User User { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}