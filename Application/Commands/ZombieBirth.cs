namespace CommeTuVeux2.Application.Commands
{
    using CommeTuVeux2.Domain.Interface;
    using CommeTuVeux2.Domain.Model.ZombieAggregate.Entities;
    using System;

    public class ZombieBirth
    {
        private readonly IEventBus _bus;

        public ZombieBirth(IEventBus bus)
        {
            _bus = bus;
        }

        public void Execute(Guid id, string zombieName)
        {
            if (zombieName.Length < 3)
                throw new Exception();

            new Zombie(id, zombieName, _bus);
        }
    }
}
