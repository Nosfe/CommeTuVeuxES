namespace CommeTuVeux2.Domain.Interface
{
    public interface IEventStream
    {
        void Add(IDomainEvent evt);
    }
}
