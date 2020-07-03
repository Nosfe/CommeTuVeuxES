namespace CommeTuVeux2.Domain.Model.ZombieAggregate.Events
{
    using CommeTuVeux2.Domain.Interface;
    using System;

    public class Walked : IDomainEvent
    {
        public Walked(Guid aggregateId, string aggregateName, int aggregateVersion, int milesWalked, int remainingHp)
        {
            AggregateId = aggregateId;
            AggregateName = aggregateName;
            AggregateVersion = aggregateVersion;
            MilesWalked = milesWalked;
            RemainingHp = remainingHp;
        }

        public string EventName
            => GetType().FullName;

        public Guid AggregateId { get; private set; }

        public string AggregateName { get; private set; }

        public int AggregateVersion { get; private set; }

        public int MilesWalked { get; set; }

        public int RemainingHp { get; set; }
    }
}
