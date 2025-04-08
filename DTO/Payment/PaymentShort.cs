namespace imsapi.DTO.Payment
{
    public class PaymentShort
    {
        public int Id { get; set; }
        public string? PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}