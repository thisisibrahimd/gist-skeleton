using Ardalis.GuardClauses;
using Newtonsoft.Json;

namespace Gist.ApplicationCore.Models
{
    public class Score
    {
        [JsonConstructor]
        public Score(double value)
        {
            Guard.Against.Negative(value, nameof(value));
            Value = value;
        }

        [JsonProperty("value")]
        public double Value { get; private set; }

        public bool IsZero()
        {
            return Value == 0;
        }
    }
}