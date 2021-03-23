using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gist.ApplicationCore.Entities;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gist.Infrastructure.Data
{
    public class PersonRepository : EhrRepository<Person>, IPersonRepository
    {
        public PersonRepository(EhrContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<Person>> GetPeopleEmrByConceptIds(int[]
            conceptIds)
        {
            return await _dbContext.People
                .Include(person => person.ProcedureOccurrences.Where(procedureOccurrence =>
                    conceptIds.Contains(procedureOccurrence.ProcedureConceptId)))
                .Include(person => person.DrugExposures.Where(drugExposure =>
                    conceptIds.Contains(drugExposure.DrugConceptId)))
                .Include(person => person.Observations.Where(observation =>
                    conceptIds.Contains(observation.ObservationConceptId)))
                .Include(person => person.ConditionOccurrences.Where(conditionOccurrence =>
                    conceptIds.Contains(conditionOccurrence.ConditionConceptId)))
                .Include(person => person.Measurements.Where(measurement =>
                    conceptIds.Contains(measurement.MeasurementConceptId)))
                .AsSplitQuery()
                .ToListAsync();
        }
    }
}