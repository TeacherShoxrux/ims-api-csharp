namespace imsapi.DTO.Payment
{
    public class PaymentShort
    {
        internal int customerId;

        public int Id { get; set; }
        public string? PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public string? customerName { get; set; }
        public string? userFullName { get; set; }
        public string? customerPhone { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}