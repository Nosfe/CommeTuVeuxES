namespace CommeTuVeux2.Domain.Model.ZombieAggregate.Entities
{
    using CommeTuVeux2.Domain.Interface;
    using CommeTuVeux2.Domain.Model.ZombieAggregate.Events;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Zombie
    {
        private const int _birthLimbNumber = 4;
        private const int _baseHp = 784512;

        public Guid Id { get; private set; }

        public int Version { get; private set; }

        public string AggregateName { get; private set; }

        public string Name { get; private set; }

        public int LimbNumber { get; private set; }

        public int MaxHp { get; private set; }

        public int CurrentHp { get; private set; }

        private Zombie()
        {

        }

        public Zombie(Guid id, string name, IEventBus bus)
        {
            name = $"fuç_soijk{name}";

            var humanRevived = new HumanRevived(id, nameof(Zombie), 1, name, _birthLimbNumber, _baseHp);
            bus.Publish(humanRevived);
            Mutate(humanRevived);
        }

        private void Mutate(IDomainEvent evt) 
        {
            switch (evt)
            {
                case HumanRevived e:
                    Id = e.AggregateId;
                    Version = e.AggregateVersion;
                    AggregateName = e.AggregateName;
                    Name = e.Name;
                    LimbNumber = e.LimbNumber;
                    MaxHp = e.Hp;
                    CurrentHp = e.Hp;
                    break;
                case LimbLost e:
                    LimbNumber = e.LimbLeft;
                    Version = e.AggregateVersion;
                    break;
                case Walked e:
                    Version = e.AggregateVersion;
                    CurrentHp = e.RemainingHp;
                    break;
                default:
                    throw new Exception();
            }
        }

        public static Zombie Hydrate(IEnumerable<IDomainEvent> evts) 
        {
            var zombie = new Zombie();

            foreach (var evt in evts.OrderBy(e => e.AggregateVersion))
            {
                zombie.Mutate(evt);
            }

            return zombie;
        }

        public void Walk(int milesNumber, IEventBus bus) 
        {
            if (milesNumber * 100 > CurrentHp)
            {
                throw new Exception("Niquezvous");
            }

            var walkingEvt = new Walked(Id, AggregateName, Version+1, milesNumber, CurrentHp - (milesNumber * 100));
            bus.Publish(walkingEvt);
            Mutate(walkingEvt);

            if (new Random().Next() % 2 == 0 && milesNumber > 10)
            {
                var lostedEvt = new LimbLost(Id, AggregateName, Version + 1, LimbNumber - 1);
                bus.Publish(lostedEvt);
                Mutate(lostedEvt);
            }
        }

        public override string ToString()
        {
            return $"zName : {Name}, Hp {CurrentHp}/{MaxHp}, Limbs : {LimbNumber}";
        }
    }
}
