

namespace imsapi.Data.Entities
{
    public class PaymentItem: EntityBase
    {
        public int paymentId { get; set; }
        public virtual Payment? Payment { get; set; }
        public int productId { get; set; }
        public virtual Product? Product { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
        public decimal totalPrice { get; set; }
        public string? description { get; set; }

    }
}