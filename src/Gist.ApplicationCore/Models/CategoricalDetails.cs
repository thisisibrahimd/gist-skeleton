using Ardalis.GuardClauses;
using Newtonsoft.Json;

namespace Gist.ApplicationCore.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CategoricalDetails
    {
        [JsonConstructor]
        public CategoricalDetails(int present)
        {
            Guard.Against.Negative(present, nameof(present));
            
            Present = present;
        }

        [JsonProperty("present", Required = Required.Always)]
        public int Present { get; set; }
    }
}