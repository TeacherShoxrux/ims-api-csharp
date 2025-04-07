namespace imsapi.Data.Entities
{
    public abstract class EntityBase
    {
        public int id { get; set; }
        public DateTime createdAt { get; set; } = DateTime.UtcNow;
        public DateTime? updatedAt { get; set; }
    }
}