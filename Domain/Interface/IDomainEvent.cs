namespace CommeTuVeux2.Domain.Interface
{
    using System;

    public interface IDomainEvent
    {
        Guid AggregateId { get; }

        string AggregateName { get; }

        string EventName { get; }

        int AggregateVersion { get; }
    }
}