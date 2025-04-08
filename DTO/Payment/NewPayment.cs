using imsapi.DTO.Payment;
using IMSAPI.DTO.Products;

namespace imsapi.DTO;
public class NewPayment
{
    public int customerId { get; set; }
    public List<PaymentProduct> poducts { get; set; }
    public string? paymentMethod { get; set; }
}