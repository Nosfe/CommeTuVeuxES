namespace CommeTuVeux2.Infrastructure
{
    using CommeTuVeux2.Domain.Interface;

    public class FileSystemEventStream : IEventStream
    {
        private readonly IDatabase _db;

        public FileSystemEventStream(IDatabase db)
        {
            _db = db;
        }

        public void Add(IDomainEvent evt)
        {
            _db.Add(evt);
        }
    }
}
