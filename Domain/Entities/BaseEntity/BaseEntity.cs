using Domain.Entities.User;

namespace Domain.Entities.BaseEntity
{
    public class BaseEntity : EntityWithNotifications
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.MinValue;
        public DateTime UpdatedAt { get; set; } = DateTime.MinValue;
        public string? CreatedById { get; set; }
        public ApplicationUser? CreatedBy { get; set; }
        public string? UpdatedById { get; set; }
        public ApplicationUser? UpdatedBy { get; set; }
    }
}
