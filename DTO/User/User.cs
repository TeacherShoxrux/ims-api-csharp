using System.ComponentModel.DataAnnotations;

namespace IMSAPI.DTO.User
{
    public class User
    {
        public int id { get; set; }
        [Phone]
        public string? phone { get; set; }
        public string? fullName { get; set; }
        public string? Password { get; set; }
        public string? storeName { get; set; }
        public string? storeImage { get; set; }
        public string? image { get; set; }
        public string? email { get; set; }
        public string? role { get; set; }
        public string? storeAddress { get; internal set; }
    }
}