using Ardalis.GuardClauses;
using Newtonsoft.Json;

namespace Gist.ApplicationCore.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class EligibilityCriterion
    {
        [JsonConstructor]
        public EligibilityCriterion(int conceptId, string domainId,
            EligibilityDetails eligibilityDetails)
        {
            Guard.Against.Negative(conceptId, nameof(conceptId));
            Guard.Against.NullOrEmpty(domainId, nameof(domainId));
            Guard.Against.Null(eligibilityDetails, nameof(eligibilityDetails));

            ConceptId = conceptId;
            DomainId = domainId;
            EligibilityDetails = eligibilityDetails;
        }

        [JsonRequired]
        [JsonProperty("concept_id")]
        public int ConceptId { get; private set; }

        [JsonRequired]
        [JsonProperty("domain_id")]
        public string DomainId { get; private set; }

        [JsonRequired]
        [JsonProperty("eligibility")]
        public EligibilityDetails EligibilityDetails { get; private set; }

        public bool GetIsAgeCriterion() => ConceptId == 4265453;

        public bool GetIsGenderCriterion() => ConceptId == 4135376;

        public bool GetIsCategoricalCriterion()
        {
            switch (DomainId)
            {
                case "Condition":
                case "Drug":
                case "Procedure":
                case "Observation" when !GetIsAgeCriterion():
                    return true;
            }

            return false;
        }

        public bool GetIsLaboratoryCriterion() => DomainId == "Measurement" || GetIsAgeCriterion();
    }
}