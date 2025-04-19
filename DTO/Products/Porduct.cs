namespace IMSAPI.DTO.Products
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public string? categoryName { get; set; }
        public decimal salePrice { get; set; }
        public decimal purchasePrice { get; set; }
        public int quantity { get; set; }
        public string? image { get; set; }
    }
}