namespace IMSAPI.DTO.User
{
    public class Session
    {
        public string SessionId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }

        public bool IsExpired => DateTime.UtcNow > ExpiresAt;
    }
}