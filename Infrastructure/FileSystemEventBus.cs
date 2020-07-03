namespace CommeTuVeux2.Infrastructure
{
    using CommeTuVeux2.Domain.Interface;
    using CommeTuVeux2.Domain.Model.Projections;
    using System.Collections.Generic;

    public class FileSystemEventBus : IEventBus
    {
        private readonly IEventStream _eventStream;
        public IList<IEventHandler> _eventHandlers { get; set; }

        public FileSystemEventBus(
            IProjectionRepository projectionRepository,
            IEventStream eventStream)
        {
            _eventStream = eventStream;
            _eventHandlers = new List<IEventHandler>();

            _eventHandlers.Add(new ZombieProjectionHandler(
                projectionRepository));
        }

        public void Publish(IDomainEvent evt)
        {
            _eventStream.Add(evt);

            foreach (var handler in _eventHandlers)
            {
                handler.Handle(evt);
            }
        }
    }
}
