namespace CommeTuVeux2.Domain.Interface
{
    public interface IEventBus
    {
        void Publish(IDomainEvent evt);
    }
}
