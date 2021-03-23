using Gist.ApplicationCore.Entities;
using Ardalis.Specification;

namespace Gist.ApplicationCore.Specification
{
    public sealed class EligibilityCriterionByTrialSpec : Specification<EligibilityCriterionEntity>
    {
        public EligibilityCriterionByTrialSpec(string trialId)
        {
            Query.Where(ec => ec.NctId == trialId);
        }
    }
}