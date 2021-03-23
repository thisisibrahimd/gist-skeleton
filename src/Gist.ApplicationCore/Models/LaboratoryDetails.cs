using Newtonsoft.Json;

namespace Gist.ApplicationCore.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class LaboratoryDetails
    {
        [JsonConstructor]
        public LaboratoryDetails(double min, double max)
        {
            Min = min;
            Max = max;
        }

        [JsonProperty("min", Required = Required.Always)]
        public double Min { get; set; }

        [JsonProperty("max", Required = Required.Always)]
        public double Max { get; set; }
    }
}