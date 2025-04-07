namespace imsapi.DTO.Category
{
    public class Category
    {
        public int id { get; set; }
        public int storeId { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public string? image { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
    }
}