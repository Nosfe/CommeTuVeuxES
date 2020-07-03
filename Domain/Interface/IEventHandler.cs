namespace CommeTuVeux2.Domain.Interface
{
    public interface IEventHandler
    {
        void Handle<T>(T evt);
    }
}
