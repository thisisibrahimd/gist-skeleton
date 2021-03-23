using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Newtonsoft.Json;

namespace Gist.ApplicationCore.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class EligibilityCriteria
    {
        [JsonConstructor]
        public EligibilityCriteria(int trialId, EligibilityCriterion[] criteria)
        {
            Guard.Against.Negative(trialId, nameof(trialId));
            Guard.Against.NullOrEmpty(criteria, nameof(criteria));

            TrialId = trialId;
            Criteria = criteria;
        }

        [JsonProperty("trial_id", Required = Required.DisallowNull)]
        public int TrialId { get; private set; }

        [JsonProperty("criteria", Required = Required.Always)]
        public EligibilityCriterion[] Criteria { get; private set; }

        public EligibilityCriterion GetAgeCriterion()
        {
            return Criteria.FirstOrDefault(
                criterion => criterion.GetIsAgeCriterion());
        }

        public EligibilityCriterion GetGenderCriterion()
        {
            return Criteria.FirstOrDefault(criterion =>
                criterion.GetIsGenderCriterion());
        }

        public IEnumerable<EligibilityCriterion> GetCategoricalCriteria()
        {
            return Criteria.Where(criterion =>
                criterion.GetIsCategoricalCriterion());
        }

        public IEnumerable<EligibilityCriterion> GetLaboratoryCriteria()
        {
            return Criteria.Where(criterion => criterion.GetIsLaboratoryCriterion());
        }

        public IEnumerable<EligibilityCriterion>
            GetLaboratoryCriteriaWithoutAgeCriterion()
        {
            return GetLaboratoryCriteria()
                .Where(criterion => !criterion.GetIsAgeCriterion());
        }
    }
}