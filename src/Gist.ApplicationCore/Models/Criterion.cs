namespace Gist.ApplicationCore.Models
{
    public class Criterion
    {
        public string ConceptName { get; set; }
        public int ConceptId { get; set; }
        public string DomainId { get; set; }
        public int CategoricalEligibility { get; set; }
        public double LaboratoryEligibilityMinimum { get; set; }
        public double LaboratoryEligibilityMaximum { get; set; }

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