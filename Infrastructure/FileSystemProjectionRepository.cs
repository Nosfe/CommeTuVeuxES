namespace CommeTuVeux2.Infrastructure
{
    using CommeTuVeux2.Domain.Interface;
    using CommeTuVeux2.Domain.Model.Projections;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class FileSystemProjectionRepository : IProjectionRepository
    {
        //private File 

        public void AddOrUpdate<T>(T projection) where T : class, IProjection
        {
            var fileName = projection.GetType().Name;

            if (!File.Exists(fileName))
            {
                using var _ = File.Create(fileName);
            }

            var lines = new List<string>();

            foreach (var line in File.ReadAllLines(fileName))
            {
                if (!line.StartsWith(projection.Id.ToString()))
                {
                    lines.Add(line);
                }
            }

            lines.Add(Inline(projection));

            File.WriteAllLines(fileName, lines.ToArray());
        }

        private string Inline(IProjection projection)
        {
            if (projection is ZombieProjectionDTO)
            {
                var p = projection as ZombieProjectionDTO;
                return $"{p.Id}#${p.Name}#${p.LimbNumber}";
            }
            return string.Empty;
        }

        public T GetById<T>(Guid id) where T : class, IProjection
        {
            var fileName = typeof(T).Name;
            foreach (var line in File.ReadAllLines(fileName))
            {
                if (line.StartsWith(id.ToString()))
                {
                    var infos = line.Split("#$", StringSplitOptions.RemoveEmptyEntries);
                    return new ZombieProjectionDTO()
                    {
                        Id = id,
                        LimbNumber = int.Parse(infos[2]),
                        Name = infos[1]
                    } as T;
                }
            }

            throw new Exception();
        }

        public IEnumerable<T> GetAll<T>() where T : class, IProjection
        {
            var fileName = typeof(T).Name;
            foreach (var line in File.ReadAllLines(fileName))
            {
                var infos = line.Split("#$", StringSplitOptions.RemoveEmptyEntries);
                yield return new ZombieProjectionDTO()
                {
                    Id = Guid.Parse(infos[0]),
                    LimbNumber = int.Parse(infos[2]),
                    Name = infos[1]
                } as T;
            }
        }
    }
}
