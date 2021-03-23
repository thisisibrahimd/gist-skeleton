using System;
using System.Linq;
using Ardalis.GuardClauses;

namespace Gist.ApplicationCore.Models
{
    public class LaboratoryCriterionStatistics
    {
        public LaboratoryCriterionStatistics(EligibilityCriterion criterion)
        {
            Guard.Against.Null(criterion, nameof(criterion));
            Criterion = criterion;
        }

        public LaboratoryCriterionStatistics(EligibilityCriterion criterion,
            double[] values) : this(criterion)
        {
            Guard.Against.Null(values, nameof(values));

            Criterion = criterion;

            Mean = values.Average();
            IneligibilityPercentage = values.Count(value =>
            {
                return Criterion.EligibilityDetails.LaboratoryEligibilityDetails.Min >= value
                       || value >= Criterion.EligibilityDetails.LaboratoryEligibilityDetails.Max;
            }) / (double) values.Length;

            var squareDifferences = values.Select(value => Math.Pow(value - Mean, 2)).ToArray();
            StandardDeviation = Math.Sqrt(squareDifferences.Average());
            NormalizedMin = (Criterion.EligibilityDetails.LaboratoryEligibilityDetails.Min - Mean) / StandardDeviation;
            NormalizedMax = (Criterion.EligibilityDetails.LaboratoryEligibilityDetails.Max - Mean) / StandardDeviation;
        }

        public double DefaultWeightedValue => (Criterion.EligibilityDetails.LaboratoryEligibilityDetails.Max -
                                               Criterion.EligibilityDetails.LaboratoryEligibilityDetails.Min) / 2 *
                                              Criterion.EligibilityDetails.LaboratoryEligibilityDetails.Min;

        public EligibilityCriterion Criterion { get; }
        public double Mean { get; }
        public double StandardDeviation { get; }
        public double IneligibilityPercentage { get; }
        public double NormalizedMin { get; }
        public double NormalizedMax { get; }

        public double GetWeightedValue(double value)
        {
            var valueMinusMean = value - Mean;
            var stdTimesIneligPerc = -
                StandardDeviation * IneligibilityPercentage;
            return valueMinusMean / stdTimesIneligPerc;
        }
    }
}