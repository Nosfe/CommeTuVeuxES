namespace CommeTuVeux2.Domain.Interface
{
    using CommeTuVeux2.Domain.Model.Projections;
    using CommeTuVeux2.Domain.Model.ZombieAggregate.Entities;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IProjectionRepository
    {
        T GetById<T>(Guid id) where T : class, IProjection;

        void AddOrUpdate<T>(T projection) where T : class, IProjection;

        IEnumerable<T> GetAll<T>() where T : class, IProjection;
    }
}
