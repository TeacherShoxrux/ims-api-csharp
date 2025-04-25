   namespace imsapi.DTO.Payment;
   public class PaymentItem{
        public int paymentId { get; set; }
        public int productId { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
        public decimal totalPrice { get; set; }
        public string? description { get; set; }

    }