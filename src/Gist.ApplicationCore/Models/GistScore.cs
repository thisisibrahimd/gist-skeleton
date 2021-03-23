using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gist.ApplicationCore.Models
{
    public class GistScore
    {
        public GistScore(int trialId, List<SGistScore> sGistScores, MGistScore mGistScore)
        {
            TrialId = trialId;
            SGistScores = sGistScores;
            MGistScore = mGistScore;
        }

        [JsonProperty("trial_id")]
        public int TrialId { get; }

        [JsonProperty("s_gist_scores")]
        public List<SGistScore> SGistScores { get; }

        [JsonProperty("m_gist_score")]
        public MGistScore MGistScore { get; }
    }
}