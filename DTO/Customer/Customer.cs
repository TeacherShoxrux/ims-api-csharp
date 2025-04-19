namespace IMSAPI.DTO.Customer
{
    public class Customer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string? info { get; set; }
        public DateTime? createdAt { get; set; }
       
    }
}