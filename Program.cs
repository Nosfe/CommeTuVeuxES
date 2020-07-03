namespace CommeTuVeux2
{
    using CommeTuVeux2.Application.Commands;
    using CommeTuVeux2.Application.Queries;
    using CommeTuVeux2.Domain.Model.ZombieAggregate.Entities;
    using CommeTuVeux2.Domain.Model.ZombieAggregate.Events;
    using CommeTuVeux2.Infrastructure;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var db = new FileSystemDatabase();
            db.Reset();

            var evtStream = new FileSystemEventStream(db);
            var projectionRepository = new FileSystemProjectionRepository();
            var evtBus = new FileSystemEventBus(projectionRepository, evtStream);

            var zId = Guid.NewGuid();
            var zombieBirth = new ZombieBirth(evtBus);
            zombieBirth.Execute(zId, "cfvghjk");

            var zWalk = new ZombieHiking(db, evtBus);
            zWalk.Execute(zId, 97);

            zWalk = new ZombieHiking(db, evtBus);
            zWalk.Execute(zId, 1);

            zWalk = new ZombieHiking(db, evtBus);
            zWalk.Execute(zId, 20);

            zWalk = new ZombieHiking(db, evtBus);
            zWalk.Execute(zId, 9);

            zWalk = new ZombieHiking(db, evtBus);
            zWalk.Execute(zId, 7);

            var zId2 = Guid.NewGuid();

            zombieBirth = new ZombieBirth(evtBus);
            zombieBirth.Execute(zId2, "Alexandre");



            var query = new GetAllZombiesQuery(projectionRepository);

            foreach(var z in query.Execute())
            {
                Console.WriteLine($"{z.Id} {z.Name} {z.LimbNumber}");
            }
        }
    }
}
