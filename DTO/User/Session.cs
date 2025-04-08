namespace IMSAPI.DTO.User
{
    public class Session
    {
    
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string? accessToken { get; set; }
        public string? refreshoken { get; set; }

    }
}