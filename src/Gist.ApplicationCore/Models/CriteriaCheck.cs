using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Gist.ApplicationCore.Entities;

namespace Gist.ApplicationCore.Models
{
    public class CriteriaCheckByPerson
    {
        public CriteriaCheckByPerson(Person person)
        {
            Guard.Against.Null(person, nameof(person));
            Person = person;
        }

        public CriteriaCheckByPerson()
        {
        }

        public Person Person { get; }
        public List<CriterionCheck> CriterionChecks { get; set; } = new();

        public bool GetIsFullyEligible()
        {
            return CriterionChecks.All(criterionCheck =>
                criterionCheck.IsEligible);
        }

        public void AddCriterionCheck(CriterionCheck criterionCheck)
        {
            Guard.Against.Null(criterionCheck, nameof(criterionCheck));
            CriterionChecks.Add(criterionCheck);
        }
    }
}