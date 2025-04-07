using System.ComponentModel.DataAnnotations;

namespace IMSAPI.DTO.Products
{
    public class NewProduct
    {
        [Required]
        public string? name { get; set; }
        public string? description { get; set; }
        public decimal salePrice { get; set; }
        public decimal purchasePrice { get; set; }
        public int quantity { get; set; }=1;
        [Required]
        public int categoryId { get; set; }
        public string? image { get; set; }
    }
}