using IMSAPI.DTO.Customer;
using IMSAPI.DTO.Products;

namespace imsapi.DTO.Payment
{
    public class Payment
    {
        public int id { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Amount { get; set; }
        public string? paymentMethod { get; set; }
        public int userId { get; set; }

        public Customer customer { get; set; }
        public List<Product> products { get; set; }
    }
}