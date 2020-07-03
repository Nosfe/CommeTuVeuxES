using System;
using System.Collections.Generic;

namespace CommeTuVeux2.Domain.Interface
{
    public interface IDatabase
    {
        void Add(IDomainEvent evt);

        IEnumerable<IDomainEvent> Get(Guid id);

        void Reset();
    }
}
