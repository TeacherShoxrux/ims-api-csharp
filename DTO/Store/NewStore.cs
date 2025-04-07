using System.ComponentModel.DataAnnotations;

namespace imsapi.DTO.Store
{
    public class NewStore
    {

        public string storeName { get; set; }

        public string? address { get; set; }
        
        [Required]
        [Phone]
        public string phone { get; set; }
        
        [Required]
        public string password { get; set; }
        

    }
}