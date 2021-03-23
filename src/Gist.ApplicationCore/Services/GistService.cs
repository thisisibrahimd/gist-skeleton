using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Gist.ApplicationCore.Entities;
using Gist.ApplicationCore.Interfaces;
using Gist.ApplicationCore.Models;

namespace Gist.ApplicationCore.Services
{
    public class GistService : IGistService
    {
        private readonly ISvmService _svm;
        private LaboratoryCriterionStatistics[] _laboratoryCriteriaStatistics;

        public GistService(ISvmService svm)
        {
            _svm = svm;
        }

        public async Task<GistScore> GetGistScore(EligibilityCriteria criteria, Person[] people)
        {
            Guard.Against.Null(people, nameof(people));
            
            _laboratoryCriteriaStatistics = GetLaboratoryCriteriaStatistics(criteria, people).ToArray();
            var criteriaChecks = GetCriteriaChecks(criteria, people).ToArray();

            var (features, labels) = GetFeaturesAndLabels(people, _laboratoryCriteriaStatistics);

            var weights = await GetWeights(features, labels);

            var sGISTScores = GetSGistScores(criteria, criteriaChecks, weights).ToArray();

            if (sGISTScores.Any(sGistScore => sGistScore.IsZero()))
            {
                var nonZeroSGistScores = sGISTScores.Where(sGistScore => sGistScore.Value != 0).ToList();
                var filteredCriteria = criteria.Criteria.Where(criterion =>
                {
                    return nonZeroSGistScores.Any(score =>
                    {
                        return score.Criterion.Equals(criterion);
                    });
                }).ToArray();
                var revisedCriteria = new EligibilityCriteria(criteria.TrialId, filteredCriteria);

                var revisedCriteriaChecks = GetCriteriaChecks(revisedCriteria, people);

                var numFullyEligible = revisedCriteriaChecks.Count(criteriaCheck => criteriaCheck.GetIsFullyEligible());
                var sumWeights = weights.Sum();
                var revisedMGISTScore = GetMGistScore(numFullyEligible, sumWeights);

                var revisedGistResult = new GistScore(criteria.TrialId, sGISTScores.ToList(), revisedMGISTScore);
                return revisedGistResult;
            }
            else
            {
                var numFullyEligible = criteriaChecks.Count(criteriaCheck => criteriaCheck.GetIsFullyEligible());
                var sumWeights = weights.Sum();
                var mGISTScore = GetMGistScore(numFullyEligible, sumWeights);

                var gistResult = new GistScore(criteria.TrialId, sGISTScores.ToList(), mGISTScore);
                return gistResult;
            }
        }

        /// <summary>
        ///     Calculate the statistics (mean, standard deviation,
        ///     ineligibility percentage, normalized min and max) on the clinical
        ///     records located in the measurement table.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="people"></param>
        /// <returns>
        ///     A IEnumerable of <c>LaboratoryCriterionStatics</c> detailing the
        ///     statistics of each laboratory criterion
        /// </returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        private IEnumerable<LaboratoryCriterionStatistics> GetLaboratoryCriteriaStatistics(EligibilityCriteria 
        criteria, Person[] people)
        {
            Guard.Against.Null(criteria, nameof(criteria));
            Guard.Against.Null(people, nameof(people));

            var laboratoryCriteria = criteria.GetLaboratoryCriteria();

            var laboratoryCriterionStatistics = laboratoryCriteria.Select(criterion =>
                {
                    if (!criterion.GetIsAgeCriterion())
                    {
                        var measurements = people.Select(person =>
                        {
                            return person.Measurements.OrderByDescending(measurement => measurement.MeasurementDate)
                                .FirstOrDefault(measurement => measurement.MeasurementConceptId == criterion.ConceptId);
                        });

                        if (measurements.All(measurement =>
                            measurement is null))
                        {
                            return new LaboratoryCriterionStatistics(criterion);
                        }
                        
                        double[] values = measurements
                            .Where(measurement => measurement.ValueAsNumber.HasValue)
                            .Select(measurement => (double) measurement.ValueAsNumber)
                            .ToArray();

                        return new LaboratoryCriterionStatistics(criterion, values);
                    }

                    var ages = people.Select(person => (double) (DateTime.Now.Year - person.YearOfBirth))
                        .ToArray();
                    return new LaboratoryCriterionStatistics(criteria.GetAgeCriterion(), ages);
                });

            return laboratoryCriterionStatistics;
        }

        /// <summary>
        ///     Calculate the eligibility of each criterion for each person
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="people"></param>
        /// <returns></returns>
        private IEnumerable<CriteriaCheckByPerson> GetCriteriaChecks(EligibilityCriteria criteria, Person[] people)
        {
            Guard.Against.Null(criteria, nameof(criteria));
            Guard.Against.NullOrEmpty(people, nameof(people));

            var criteriaChecks = people.Select(person =>
            {
                var criteriaCheck = new CriteriaCheckByPerson(person);

                foreach (var criterion in criteria.Criteria)
                {
                    var criterionCheck = new CriterionCheck(criterion);

                    if (criterion.GetIsGenderCriterion())
                    {
                        var genderConceptId = person.GenderConceptId;
                        criterionCheck.GenderCheck(genderConceptId);
                    }
                    else if (criterion.GetIsAgeCriterion())
                    {
                        var age = DateTime.Now.Year - person.YearOfBirth;
                        criterionCheck.LaboratoryCheck(age);
                    }
                    else if (criterion.GetIsCategoricalCriterion())
                    {
                        var isPresent = false;
                        switch (criterion.DomainId)
                        {
                            case "Observation":
                                isPresent = person.Observations.Any(observation =>
                                    observation.ObservationConceptId == criterion.ConceptId);
                                break;
                            case "Condition":
                                isPresent = person.ConditionOccurrences.Any(
                                    condition => condition.ConditionConceptId == criterion.ConceptId);
                                break;
                            case "Procedure":
                                isPresent = person.ProcedureOccurrences.Any(
                                    procedure => procedure.ProcedureConceptId == criterion.ConceptId);
                                break;
                            case "Drug":
                                isPresent = person.DrugExposures.Any(drug => drug.DrugConceptId == criterion.ConceptId);
                                break;
                            default:
                                break;
                        }

                        criterionCheck.CategoricalCheck(isPresent ? 1 : 0);
                    }
                    else
                    {
                        var measurement = person.Measurements
                            .FirstOrDefault(measurement => measurement.MeasurementConceptId == criterion.ConceptId);

                        if (measurement is null)
                            criterionCheck.LaboratoryCheck(null);
                        else
                            criterionCheck.LaboratoryCheck(measurement.ValueAsNumber);
                    }

                    criteriaCheck.AddCriterionCheck(criterionCheck);
                }

                return criteriaCheck;
            });

            return criteriaChecks;
        }

        /// <summary>
        ///     Get features and labels from a list of people to be able to get Svc
        /// </summary>
        /// <param name="people"></param>
        /// <param name="laboratoryCriteriaStatistics"></param>
        /// <returns>A tuple with features and labels</returns>
        private (double[][] features, double[] labels) GetFeaturesAndLabels(Person[] people, 
            LaboratoryCriterionStatistics[] laboratoryCriteriaStatistics)
        {
            Guard.Against.NullOrEmpty(people, nameof(people));
            Guard.Against.NullOrEmpty(laboratoryCriteriaStatistics, nameof(laboratoryCriteriaStatistics));

            double[][] features = new double[people.Length][];
            double[] labels = new double[people.Length];

            for (int pIndex = 0; pIndex < people.Length; pIndex++)
            {
                Person person = people[pIndex];
                
                double[] measurements = new double[laboratoryCriteriaStatistics.Length];

                for (int lIndex = 0; lIndex < laboratoryCriteriaStatistics.Length; lIndex++)
                {
                    LaboratoryCriterionStatistics laboratoryCriterionStatistic = laboratoryCriteriaStatistics[lIndex];
                    
                    if (laboratoryCriterionStatistic.Criterion.GetIsAgeCriterion())
                    {
                        int age = DateTime.Now.Year - person.YearOfBirth;
                        double weightedAge = laboratoryCriterionStatistic.GetWeightedValue(age);
                        labels[pIndex] = weightedAge;
                    }
                    else
                    {
                        Measurement measurement = person.Measurements
                            .FirstOrDefault(meas =>
                            {
                                return meas.MeasurementConceptId == laboratoryCriterionStatistic.Criterion.ConceptId;
                            });

                        if (measurement?.ValueAsNumber != null)
                        {
                            double weightedMeasurement =
                                laboratoryCriterionStatistic.GetWeightedValue((double) measurement.ValueAsNumber);
                            measurements[lIndex] = weightedMeasurement;
                        }
                        else
                        {
                            var weightedMeasurement = laboratoryCriterionStatistic.DefaultWeightedValue;
                            measurements[lIndex] = weightedMeasurement;
                        }
                    }
                }

                features[pIndex] = measurements;
            }

            return (features, labels);
        }

        /// <summary>
        ///     Perform C-Support Vector Machine Classification on EMRs via an API.
        /// </summary>
        /// <param name="features"></param>
        /// <param name="labels"></param>
        /// <returns>An array of predications from SVC.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        private async Task<double[]> GetWeights(double[][] features,
            double[] labels)
        {
            Guard.Against.NullOrEmpty(features, nameof(features));
            Guard.Against.NullOrEmpty(labels, nameof(labels));

            var predictions = await _svm.SvcAsync(features.ToArray(), labels.ToArray());
            if (predictions == null)
            {
                throw new InvalidOperationException("Failed to get Predictions from Svm API");
            }
            ;

            var weights = predictions
                .Select((prediction, index) => (double) 1 / (1 + Math.Abs(prediction - labels[index])))
                .ToArray();

            return weights;
        }

        /// <summary>
        ///     Calculate the S GIST Scores for criteria and returns the result
        /// </summary>
        /// <param name="criteria">Eligibility Criteria </param>
        /// <param name="criteriaCheck">Criteria Checks for each person</param>
        /// <param name="weights">Weighted predictions</param>
        /// <returns>An Array of S Gist Scores index by the order of criteria.</returns>
        private IEnumerable<SGistScore> GetSGistScores(EligibilityCriteria criteria, 
            CriteriaCheckByPerson[] criteriaChecks, double[] weights)
        {
            Guard.Against.Null(criteria, nameof(criteria));
            Guard.Against.NullOrEmpty(criteriaChecks, nameof(criteriaChecks));
            Guard.Against.NullOrEmpty(weights, nameof(weights));

            SGistScore[] sGistScores = new SGistScore[criteria.Criteria.Length];

            var sumOfWeights = weights.Sum();

            for (var index = 0; index < criteria.Criteria.Length; index++)
            {
                var criterion = criteria.Criteria[index];
                
                var criterionChecks = criteriaChecks.Select(criteriaCheck =>
                {
                    return criteriaCheck.CriterionChecks.FirstOrDefault(
                        criterionCheck => criterionCheck.Criterion.ConceptId == criterion.ConceptId);
                });

                var weightsByCriterion = criterionChecks.Select((criterionCheck, index) =>
                    {
                        if (criterionCheck != null && criterionCheck.IsEligible) return weights[index];
                        return 0;
                    });

                var sGistScore = GetSGistScore(criterion, weightsByCriterion.Sum(), sumOfWeights);
                sGistScores[index] = sGistScore;
            }

            return sGistScores;
        }

        /// <summary>
        ///     Calculate S Gist Score for a criterion and returns the result
        /// </summary>
        /// <param name="criterion">An EligibilityCriterion</param>
        /// <param name="sumOfWeightedCriterionEligibility">
        ///     Sum of weighted predictions
        ///     filtered by
        /// </param>
        /// <param name="sumOfWeights">Sum of weighted predictions</param>
        /// <returns>SGistScore</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        private SGistScore GetSGistScore(EligibilityCriterion criterion, double sumOfWeightedCriterionEligibility,
            double sumOfWeights)
        {
            Guard.Against.Negative(sumOfWeightedCriterionEligibility, nameof(sumOfWeightedCriterionEligibility));
            Guard.Against.NegativeOrZero(sumOfWeights, nameof(sumOfWeights));

            if (sumOfWeightedCriterionEligibility > sumOfWeights)
                throw new ArgumentOutOfRangeException(nameof(sumOfWeightedCriterionEligibility));

            var score = sumOfWeightedCriterionEligibility / sumOfWeights;
            var sGistScore = new SGistScore(criterion, score);

            return sGistScore;
        }

        /// <summary>
        ///     Calculates M Gist Score and returns the result
        /// </summary>
        /// <param name="numOfFullyEligible">Number of fully eligible people</param>
        /// <param name="sumOfWeights">Sum of weighted predictions</param>
        /// <returns>An <c>MGistScore</c></returns>
        /// <exception cref="ArgumentException">
        ///     <c>numOfFullyEligible</c> and
        ///     <c>sumOfWeights</c> can't be zero or negative
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <c>numOfFullyEligible</c> can't be greater than the <c>sumOfWeights</c>
        /// </exception>
        private MGistScore GetMGistScore(int numOfFullyEligible, double sumOfWeights)
        {
            Guard.Against.Negative(numOfFullyEligible, nameof(numOfFullyEligible));
            Guard.Against.NegativeOrZero(sumOfWeights, nameof(sumOfWeights));

            if (numOfFullyEligible > sumOfWeights)
            {
                throw new ArgumentOutOfRangeException(nameof(numOfFullyEligible));
            }

            var score = numOfFullyEligible / sumOfWeights;
            var mGistScore = new MGistScore(score);

            return mGistScore;
        }
    }
}