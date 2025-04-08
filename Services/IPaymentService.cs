using imsapi.DTO;
using imsapi.DTO.Payment;

namespace imsapi.Services
{
    public interface IPaymentService
    {
        Task<Result<PaymentShort>> ProcessPaymentAsync(int storeId,int userId,NewPayment payment);
        Task<Result<List<PaymentShort>>> GetPaymentsListPagenatedByStoreId(int storeId, int page, int pageSize);
        Task<Result<Payment>> GetPaymentById(int id);

    }
}