namespace CommeTuVeux2.Domain.Model.ZombieAggregate.Events
{
    using CommeTuVeux2.Domain.Interface;
    using System;

    public class HumanRevived : IDomainEvent
    {
        public Guid AggregateId { get; private set; }

        public string AggregateName { get; private set; }

        public string EventName
            => GetType().FullName;

        public int AggregateVersion { get; private set; }

        public string Name { get; private set; }

        public int LimbNumber { get; private set; }

        public int Hp { get; private set; }

        public HumanRevived(Guid aggregateId, string aggregateName, int aggregateVersion, string name, int limbNumber, int hp)
        {
            AggregateId = aggregateId;
            AggregateName = aggregateName;
            AggregateVersion = aggregateVersion;

            Name = name;
            LimbNumber = limbNumber;
            Hp = hp;
        }
    }
}