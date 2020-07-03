namespace CommeTuVeux2.Domain.Model.ZombieAggregate.Events
{
    using CommeTuVeux2.Domain.Interface;
    using System;

    public class LimbLost : IDomainEvent
    {
        public LimbLost(Guid aggregateId, string aggregateName, int aggregateVersion, int limbLeft)
        {
            AggregateId = aggregateId;
            AggregateName = aggregateName;
            AggregateVersion = aggregateVersion;
            LimbLeft = limbLeft;
        }

        public string EventName
            => GetType().FullName;

        public Guid AggregateId { get; private set; }

        public string AggregateName { get; private set; }

        public int AggregateVersion { get; private set; }

        public int LimbLeft { get; set; }
    }
}
