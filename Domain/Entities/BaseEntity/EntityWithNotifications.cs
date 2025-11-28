using MediatR;

namespace Domain.Entities.BaseEntity
{
    public class EntityWithNotifications
    {
        private readonly List<INotification> _domainEvents = new();

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(INotification domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
        protected void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }
    }
}
