using CommeTuVeux2.Domain.Interface;
using CommeTuVeux2.Domain.Model.Projections;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommeTuVeux2.Application.Queries
{
    public class GetAllZombiesQuery
    {
        private readonly IProjectionRepository projectionRepository;

        public GetAllZombiesQuery(IProjectionRepository projectionRepository)
        {
            this.projectionRepository = projectionRepository;
        }

        public IEnumerable<ZombieProjectionDTO> Execute()
        {
            return projectionRepository.GetAll<ZombieProjectionDTO>();
        }
    }
}
