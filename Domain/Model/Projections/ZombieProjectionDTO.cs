namespace CommeTuVeux2.Domain.Model.Projections
{
    using CommeTuVeux2.Domain.Interface;
    using System;

    public class ZombieProjectionDTO : IProjection
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int LimbNumber { get; set; }
    }
}
