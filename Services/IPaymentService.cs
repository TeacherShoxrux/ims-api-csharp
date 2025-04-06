namespace imsapi.Services
{
    public interface IPaymentService
    {
        Task<bool> ProcessPaymentAsync(decimal amount, string paymentMethod);
        Task<decimal> GetPaymentStatusAsync(string transactionId);
        Task<bool> RefundPaymentAsync(string transactionId);
    }
}