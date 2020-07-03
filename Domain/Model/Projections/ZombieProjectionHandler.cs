namespace CommeTuVeux2.Domain.Model.Projections
{
    using CommeTuVeux2.Domain.Interface;
    using CommeTuVeux2.Domain.Model.ZombieAggregate.Events;
    using System;
    using System.Collections.Generic;

    public class ZombieProjectionHandler : IEventHandler
    {
        private readonly IProjectionRepository _projRepo;

        public IList<Type> Handlers { get; set; }

        public ZombieProjectionHandler(IProjectionRepository projRepo)
        {
            _projRepo = projRepo;
            Handlers = new List<Type>
            {
                typeof(HumanRevived),
                typeof(LimbLost),
            };
        }

        public void Handle<T>(T evt)
        {
            switch (evt)
            {
                case HumanRevived e:
                    _projRepo.AddOrUpdate(new ZombieProjectionDTO
                    {
                        Id = e.AggregateId,
                        Name = e.Name,
                        LimbNumber = e.LimbNumber,
                    });
                    break;
                case LimbLost e:
                    var zombieProj = _projRepo.GetById<ZombieProjectionDTO>(e.AggregateId);
                    zombieProj.LimbNumber = e.LimbLeft;
                    _projRepo.AddOrUpdate(zombieProj);
                    break;
            }
        }
    }
}
