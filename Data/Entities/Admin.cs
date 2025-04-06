using System;

namespace imsapi.Data.Entities
{
    public class Admin: EntityBase
    {
        public string fullName { get; set; }
        public string PasswordHash { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Image { get; set; }
    }
}