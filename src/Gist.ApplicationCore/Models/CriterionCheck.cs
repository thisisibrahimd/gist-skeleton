using Ardalis.GuardClauses;

namespace Gist.ApplicationCore.Models
{
    public class CriterionCheck
    {
        public CriterionCheck(EligibilityCriterion criterion)
        {
            Guard.Against.Null(criterion, nameof(criterion));
            Criterion = criterion;
        }

        public EligibilityCriterion Criterion { get; }
        public bool IsEligible { get; private set; }

        public void GenderCheck(int genderConceptId)
        {
            Guard.Against.Null(genderConceptId, nameof(genderConceptId));

            switch (Criterion.EligibilityDetails.CategoricalEligibilityDetails
                .Present)
            {
                case 0:
                    IsEligible = true;
                    break;
                case 1 when genderConceptId == 8507:
                    IsEligible = true;
                    break;
                case 2 when genderConceptId == 8532:
                    IsEligible = true;
                    break;
                default:
                    IsEligible = false;
                    break;
            }
        }

        public void CategoricalCheck(int present)
        {
            if (present == Criterion.EligibilityDetails
                .CategoricalEligibilityDetails.Present)
                IsEligible = true;
            else if (present == -1 && Criterion.EligibilityDetails
                .CategoricalEligibilityDetails.Present == 0)
                IsEligible = true;
            else
                IsEligible = false;
        }

        public void LaboratoryCheck(decimal? value)
        {
            if (value.HasValue)
                switch (Criterion.EligibilityDetails
                    .CategoricalEligibilityDetails.Present)
                {
                    case 1 when (double) value >= Criterion.EligibilityDetails
                                    .LaboratoryEligibilityDetails.Min
                                && (double) value <= Criterion
                                    .EligibilityDetails
                                    .LaboratoryEligibilityDetails.Max:
                        IsEligible = true;
                        break;
                    case 0 when (double) value < Criterion.EligibilityDetails
                                    .LaboratoryEligibilityDetails.Min
                                || (double) value > Criterion.EligibilityDetails
                                    .LaboratoryEligibilityDetails.Max:
                        IsEligible = true;
                        break;
                    default:
                        IsEligible = false;
                        break;
                }
            else
                IsEligible = false;
        }
    }
}