using Ardalis.GuardClauses;
using Newtonsoft.Json;

namespace Gist.ApplicationCore.Models
{
    public class SGistScore : Score
    {
        public SGistScore(EligibilityCriterion criterion, double value) : base(
            value)
        {
            Guard.Against.Null(criterion, nameof(criterion));
            Criterion = criterion;
        }

        [JsonProperty("criterion")]
        public EligibilityCriterion Criterion { get; private set; }
    }
}