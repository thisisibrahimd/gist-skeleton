using Ardalis.GuardClauses;
using Newtonsoft.Json;

namespace Gist.ApplicationCore.Models
{
    public class EligibilityDetails
    {
        public EligibilityDetails()
        {
        }

        public EligibilityDetails(
            CategoricalDetails categoricalEligibilityDetails)
        {
            Guard.Against.Null(categoricalEligibilityDetails,
                nameof(categoricalEligibilityDetails));

            CategoricalEligibilityDetails = categoricalEligibilityDetails;
        }

        public EligibilityDetails(
            CategoricalDetails categoricalEligibilityDetails,
            LaboratoryDetails laboratoryEligibilityDetails) : this(
            categoricalEligibilityDetails)
        {
            Guard.Against.Null(laboratoryEligibilityDetails,
                nameof(laboratoryEligibilityDetails));

            LaboratoryEligibilityDetails = laboratoryEligibilityDetails;
        }

        [JsonRequired]
        [JsonProperty("cat_elig")]
        public CategoricalDetails CategoricalEligibilityDetails { get; set; }

        [JsonProperty("lab_elig")]
        public LaboratoryDetails LaboratoryEligibilityDetails { get; set; }
    }
}