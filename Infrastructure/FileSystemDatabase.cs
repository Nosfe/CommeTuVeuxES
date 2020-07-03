namespace CommeTuVeux2.Infrastructure
{
    using CommeTuVeux2.Domain.Interface;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class FileSystemDatabase : IDatabase
    {
        private string FileName = @"toto.txt";

        public void Add(IDomainEvent evt)
        {
            if (!File.Exists(FileName))
            {
                using var _ = File.Create(FileName);
            }

            File.AppendAllLines(FileName, new string[] { Inline(evt) });
        }

        [Obsolete]
        public IEnumerable<IDomainEvent> Get(Guid aggregateId)
        {
            var lines = File.ReadAllLines(FileName).Where(l => l.StartsWith(aggregateId.ToString()));

            var serializer = new JSonSerializer();
            foreach (var line in lines)
            {
                var lineDetails = line.Split("#$", StringSplitOptions.RemoveEmptyEntries);
                yield return (IDomainEvent)serializer.Deserialize(GetType(lineDetails[3]), lineDetails[4]);
            }
        }


        public void Reset() => File.Delete(FileName);

        private string Inline(IDomainEvent evt)
            => $"{evt.AggregateId}#${evt.AggregateName}#${evt.AggregateVersion}#${evt.EventName}#${Serialize(evt)}";

        private string Serialize(IDomainEvent evt)
        {
            return new JSonSerializer().Serialize(evt);
        }
        
        private Type GetType(string typeName)
        {
            var type = Type.GetType(typeName);
            if (type != null)
            {
                return type;
            }

            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = a.GetType(typeName);
                if (type != null)
                {
                    return type;
                }
            }
            return null;
        }
    }
}
