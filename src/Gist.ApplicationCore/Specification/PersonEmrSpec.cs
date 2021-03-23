using Ardalis.Specification;
using Gist.ApplicationCore.Entities;

namespace Gist.ApplicationCore.Specification
{
    public sealed class PersonEmrSpec : Specification<Person>
    {
        public PersonEmrSpec()
        {
            Query
                .Include(person => person.ConditionOccurrences)
                .Include(person => person.DrugExposures)
                .Include(person => person.ProcedureOccurrences)
                .Include(person => person.Measurements)
                .Include(person => person.Observations)
                .OrderBy(person => person.PersonId);
        }

        public PersonEmrSpec(int id)
        {
            Query
                .Where(person => person.PersonId == id)
                .Include(person => person.ConditionOccurrences)
                .Include(person => person.DrugExposures)
                .Include(person => person.ProcedureOccurrences)
                .Include(person => person.Measurements)
                .Include(person => person.Observations);
        }
    }
}