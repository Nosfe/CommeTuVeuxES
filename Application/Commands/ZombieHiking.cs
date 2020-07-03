namespace CommeTuVeux2.Application.Commands
{
    using CommeTuVeux2.Domain.Interface;
    using CommeTuVeux2.Domain.Model.ZombieAggregate.Entities;
    using System;

    public class ZombieHiking
    {
        private readonly IDatabase _db;
        private readonly IEventBus _bus;

        public ZombieHiking(IDatabase db, IEventBus bus)
        {
            _db = db;
            _bus = bus;
        }

        public void Execute(Guid zombieId, int milesWalked) 
        {
            var evts = _db.Get(zombieId);
            var zombie = Zombie.Hydrate(evts);

            zombie.Walk(milesWalked, _bus);
            Console.WriteLine(zombie.ToString());
        }
    }
}
