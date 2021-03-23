using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("concept")]
    [Index(nameof(ConceptClassId), Name = "idx_concept_class_id")]
    [Index(nameof(ConceptCode), Name = "idx_concept_code")]
    [Index(nameof(ConceptId), Name = "idx_concept_concept_id", IsUnique = true)]
    [Index(nameof(DomainId), Name = "idx_concept_domain_id")]
    [Index(nameof(VocabularyId), Name = "idx_concept_vocabluary_id")]
    public class Concept : BaseEntity, IAggregateRoot
    {
        public Concept()
        {
            CareSites = new HashSet<CareSite>();
            CohortAttributes = new HashSet<CohortAttribute>();
            CohortDefinitions = new HashSet<CohortDefinition>();
            ConceptAncestorAncestorConcepts = new HashSet<ConceptAncestor>();
            ConceptAncestorDescendantConcepts = new HashSet<ConceptAncestor>();
            ConceptClasses = new HashSet<ConceptClass>();
            ConceptRelationshipConceptId1Navigations = new HashSet<ConceptRelationship>();
            ConceptRelationshipConceptId2Navigations = new HashSet<ConceptRelationship>();
            ConditionEras = new HashSet<ConditionEra>();
            ConditionOccurrenceConditionConcepts = new HashSet<ConditionOccurrence>();
            ConditionOccurrenceConditionSourceConcepts = new HashSet<ConditionOccurrence>();
            ConditionOccurrenceConditionStatusConcepts = new HashSet<ConditionOccurrence>();
            ConditionOccurrenceConditionTypeConcepts = new HashSet<ConditionOccurrence>();
            CostCurrencyConcepts = new HashSet<Cost>();
            CostDrgConcepts = new HashSet<Cost>();
            DeathCauseConcepts = new HashSet<Death>();
            DeathCauseSourceConcepts = new HashSet<Death>();
            DeathDeathTypeConcepts = new HashSet<Death>();
            DeviceExposureDeviceConcepts = new HashSet<DeviceExposure>();
            DeviceExposureDeviceSourceConcepts = new HashSet<DeviceExposure>();
            DeviceExposureDeviceTypeConcepts = new HashSet<DeviceExposure>();
            Domains = new HashSet<Domain>();
            DoseEraDrugConcepts = new HashSet<DoseEra>();
            DoseEraUnitConcepts = new HashSet<DoseEra>();
            DrugEras = new HashSet<DrugEra>();
            DrugExposureDrugConcepts = new HashSet<DrugExposure>();
            DrugExposureDrugSourceConcepts = new HashSet<DrugExposure>();
            DrugExposureDrugTypeConcepts = new HashSet<DrugExposure>();
            DrugExposureRouteConcepts = new HashSet<DrugExposure>();
            DrugStrengthAmountUnitConcepts = new HashSet<DrugStrength>();
            DrugStrengthDenominatorUnitConcepts = new HashSet<DrugStrength>();
            DrugStrengthDrugConcepts = new HashSet<DrugStrength>();
            DrugStrengthIngredientConcepts = new HashSet<DrugStrength>();
            DrugStrengthNumeratorUnitConcepts = new HashSet<DrugStrength>();
            MeasurementMeasurementConcepts = new HashSet<Measurement>();
            MeasurementMeasurementSourceConcepts = new HashSet<Measurement>();
            MeasurementMeasurementTypeConcepts = new HashSet<Measurement>();
            MeasurementOperatorConcepts = new HashSet<Measurement>();
            MeasurementUnitConcepts = new HashSet<Measurement>();
            MeasurementValueAsConcepts = new HashSet<Measurement>();
            NoteEncodingConcepts = new HashSet<Note>();
            NoteLanguageConcepts = new HashSet<Note>();
            NoteNlpNoteNlpConcepts = new HashSet<NoteNlp>();
            NoteNlpSectionConcepts = new HashSet<NoteNlp>();
            NoteNoteClassConcepts = new HashSet<Note>();
            NoteNoteTypeConcepts = new HashSet<Note>();
            ObservationObservationConcepts = new HashSet<Observation>();
            ObservationObservationSourceConcepts = new HashSet<Observation>();
            ObservationObservationTypeConcepts = new HashSet<Observation>();
            ObservationPeriods = new HashSet<ObservationPeriod>();
            ObservationQualifierConcepts = new HashSet<Observation>();
            ObservationUnitConcepts = new HashSet<Observation>();
            ObservationValueAsConcepts = new HashSet<Observation>();
            PersonEthnicityConcepts = new HashSet<Person>();
            PersonEthnicitySourceConcepts = new HashSet<Person>();
            PersonGenderConcepts = new HashSet<Person>();
            PersonGenderSourceConcepts = new HashSet<Person>();
            PersonRaceConcepts = new HashSet<Person>();
            PersonRaceSourceConcepts = new HashSet<Person>();
            ProcedureOccurrenceModifierConcepts = new HashSet<ProcedureOccurrence>();
            ProcedureOccurrenceProcedureConcepts = new HashSet<ProcedureOccurrence>();
            ProcedureOccurrenceProcedureSourceConcepts = new HashSet<ProcedureOccurrence>();
            ProcedureOccurrenceProcedureTypeConcepts = new HashSet<ProcedureOccurrence>();
            ProviderGenderConcepts = new HashSet<Provider>();
            ProviderGenderSourceConcepts = new HashSet<Provider>();
            ProviderSpecialtyConcepts = new HashSet<Provider>();
            ProviderSpecialtySourceConcepts = new HashSet<Provider>();
            Relationships = new HashSet<Relationship>();
            SourceToConceptMaps = new HashSet<SourceToConceptMap>();
            SpecimanAnatomicSiteConcepts = new HashSet<Speciman>();
            SpecimanDiseaseStatusConcepts = new HashSet<Speciman>();
            SpecimanSpecimenConcepts = new HashSet<Speciman>();
            SpecimanSpecimenTypeConcepts = new HashSet<Speciman>();
            SpecimanUnitConcepts = new HashSet<Speciman>();
            VisitOccurrenceAdmittingSourceConcepts = new HashSet<VisitOccurrence>();
            VisitOccurrenceDischargeToConcepts = new HashSet<VisitOccurrence>();
            VisitOccurrenceVisitConcepts = new HashSet<VisitOccurrence>();
            VisitOccurrenceVisitSourceConcepts = new HashSet<VisitOccurrence>();
            VisitOccurrenceVisitTypeConcepts = new HashSet<VisitOccurrence>();
            Vocabularies = new HashSet<Vocabulary>();
        }

        [Key]
        [Column("concept_id")]
        public int ConceptId { get; set; }

        [Required]
        [Column("concept_name")]
        [StringLength(255)]
        public string ConceptName { get; set; }

        [Required]
        [Column("domain_id")]
        [StringLength(20)]
        public string DomainId { get; set; }

        [Required]
        [Column("vocabulary_id")]
        [StringLength(20)]
        public string VocabularyId { get; set; }

        [Required]
        [Column("concept_class_id")]
        [StringLength(20)]
        public string ConceptClassId { get; set; }

        [Column("standard_concept")]
        [StringLength(1)]
        public string StandardConcept { get; set; }

        [Required]
        [Column("concept_code")]
        [StringLength(50)]
        public string ConceptCode { get; set; }

        [Column("valid_start_date", TypeName = "date")]
        public DateTime ValidStartDate { get; set; }

        [Column("valid_end_date", TypeName = "date")]
        public DateTime ValidEndDate { get; set; }

        [Column("invalid_reason")]
        [StringLength(1)]
        public string InvalidReason { get; set; }

        [ForeignKey(nameof(ConceptClassId))]
        [InverseProperty("Concepts")]
        public virtual ConceptClass ConceptClass { get; set; }

        [ForeignKey(nameof(DomainId))]
        [InverseProperty("Concepts")]
        public virtual Domain Domain { get; set; }

        [ForeignKey(nameof(VocabularyId))]
        [InverseProperty("Concepts")]
        public virtual Vocabulary Vocabulary { get; set; }

        [InverseProperty(nameof(CareSite.PlaceOfServiceConcept))]
        public virtual ICollection<CareSite> CareSites { get; set; }

        [InverseProperty(nameof(CohortAttribute.ValueAsConcept))]
        public virtual ICollection<CohortAttribute> CohortAttributes { get; set; }

        [InverseProperty(nameof(CohortDefinition.DefinitionTypeConcept))]
        public virtual ICollection<CohortDefinition> CohortDefinitions { get; set; }

        [InverseProperty(nameof(ConceptAncestor.AncestorConcept))]
        public virtual ICollection<ConceptAncestor> ConceptAncestorAncestorConcepts { get; set; }

        [InverseProperty(nameof(ConceptAncestor.DescendantConcept))]
        public virtual ICollection<ConceptAncestor> ConceptAncestorDescendantConcepts { get; set; }

        [InverseProperty("ConceptClassConcept")]
        public virtual ICollection<ConceptClass> ConceptClasses { get; set; }

        [InverseProperty(nameof(ConceptRelationship.ConceptId1Navigation))]
        public virtual ICollection<ConceptRelationship> ConceptRelationshipConceptId1Navigations { get; set; }

        [InverseProperty(nameof(ConceptRelationship.ConceptId2Navigation))]
        public virtual ICollection<ConceptRelationship> ConceptRelationshipConceptId2Navigations { get; set; }

        [InverseProperty(nameof(ConditionEra.ConditionConcept))]
        public virtual ICollection<ConditionEra> ConditionEras { get; set; }

        [InverseProperty(nameof(ConditionOccurrence.ConditionConcept))]
        public virtual ICollection<ConditionOccurrence> ConditionOccurrenceConditionConcepts { get; set; }

        [InverseProperty(nameof(ConditionOccurrence.ConditionSourceConcept))]
        public virtual ICollection<ConditionOccurrence> ConditionOccurrenceConditionSourceConcepts { get; set; }

        [InverseProperty(nameof(ConditionOccurrence.ConditionStatusConcept))]
        public virtual ICollection<ConditionOccurrence> ConditionOccurrenceConditionStatusConcepts { get; set; }

        [InverseProperty(nameof(ConditionOccurrence.ConditionTypeConcept))]
        public virtual ICollection<ConditionOccurrence> ConditionOccurrenceConditionTypeConcepts { get; set; }

        [InverseProperty(nameof(Cost.CurrencyConcept))]
        public virtual ICollection<Cost> CostCurrencyConcepts { get; set; }

        [InverseProperty(nameof(Cost.DrgConcept))]
        public virtual ICollection<Cost> CostDrgConcepts { get; set; }

        [InverseProperty(nameof(Death.CauseConcept))]
        public virtual ICollection<Death> DeathCauseConcepts { get; set; }

        [InverseProperty(nameof(Death.CauseSourceConcept))]
        public virtual ICollection<Death> DeathCauseSourceConcepts { get; set; }

        [InverseProperty(nameof(Death.DeathTypeConcept))]
        public virtual ICollection<Death> DeathDeathTypeConcepts { get; set; }

        [InverseProperty(nameof(DeviceExposure.DeviceConcept))]
        public virtual ICollection<DeviceExposure> DeviceExposureDeviceConcepts { get; set; }

        [InverseProperty(nameof(DeviceExposure.DeviceSourceConcept))]
        public virtual ICollection<DeviceExposure> DeviceExposureDeviceSourceConcepts { get; set; }

        [InverseProperty(nameof(DeviceExposure.DeviceTypeConcept))]
        public virtual ICollection<DeviceExposure> DeviceExposureDeviceTypeConcepts { get; set; }

        [InverseProperty("DomainConcept")]
        public virtual ICollection<Domain> Domains { get; set; }

        [InverseProperty(nameof(DoseEra.DrugConcept))]
        public virtual ICollection<DoseEra> DoseEraDrugConcepts { get; set; }

        [InverseProperty(nameof(DoseEra.UnitConcept))]
        public virtual ICollection<DoseEra> DoseEraUnitConcepts { get; set; }

        [InverseProperty(nameof(DrugEra.DrugConcept))]
        public virtual ICollection<DrugEra> DrugEras { get; set; }

        [InverseProperty(nameof(DrugExposure.DrugConcept))]
        public virtual ICollection<DrugExposure> DrugExposureDrugConcepts { get; set; }

        [InverseProperty(nameof(DrugExposure.DrugSourceConcept))]
        public virtual ICollection<DrugExposure> DrugExposureDrugSourceConcepts { get; set; }

        [InverseProperty(nameof(DrugExposure.DrugTypeConcept))]
        public virtual ICollection<DrugExposure> DrugExposureDrugTypeConcepts { get; set; }

        [InverseProperty(nameof(DrugExposure.RouteConcept))]
        public virtual ICollection<DrugExposure> DrugExposureRouteConcepts { get; set; }

        [InverseProperty(nameof(DrugStrength.AmountUnitConcept))]
        public virtual ICollection<DrugStrength> DrugStrengthAmountUnitConcepts { get; set; }

        [InverseProperty(nameof(DrugStrength.DenominatorUnitConcept))]
        public virtual ICollection<DrugStrength> DrugStrengthDenominatorUnitConcepts { get; set; }

        [InverseProperty(nameof(DrugStrength.DrugConcept))]
        public virtual ICollection<DrugStrength> DrugStrengthDrugConcepts { get; set; }

        [InverseProperty(nameof(DrugStrength.IngredientConcept))]
        public virtual ICollection<DrugStrength> DrugStrengthIngredientConcepts { get; set; }

        [InverseProperty(nameof(DrugStrength.NumeratorUnitConcept))]
        public virtual ICollection<DrugStrength> DrugStrengthNumeratorUnitConcepts { get; set; }

        [InverseProperty(nameof(Measurement.MeasurementConcept))]
        public virtual ICollection<Measurement> MeasurementMeasurementConcepts { get; set; }

        [InverseProperty(nameof(Measurement.MeasurementSourceConcept))]
        public virtual ICollection<Measurement> MeasurementMeasurementSourceConcepts { get; set; }

        [InverseProperty(nameof(Measurement.MeasurementTypeConcept))]
        public virtual ICollection<Measurement> MeasurementMeasurementTypeConcepts { get; set; }

        [InverseProperty(nameof(Measurement.OperatorConcept))]
        public virtual ICollection<Measurement> MeasurementOperatorConcepts { get; set; }

        [InverseProperty(nameof(Measurement.UnitConcept))]
        public virtual ICollection<Measurement> MeasurementUnitConcepts { get; set; }

        [InverseProperty(nameof(Measurement.ValueAsConcept))]
        public virtual ICollection<Measurement> MeasurementValueAsConcepts { get; set; }

        [InverseProperty(nameof(Note.EncodingConcept))]
        public virtual ICollection<Note> NoteEncodingConcepts { get; set; }

        [InverseProperty(nameof(Note.LanguageConcept))]
        public virtual ICollection<Note> NoteLanguageConcepts { get; set; }

        [InverseProperty(nameof(NoteNlp.NoteNlpConcept))]
        public virtual ICollection<NoteNlp> NoteNlpNoteNlpConcepts { get; set; }

        [InverseProperty(nameof(NoteNlp.SectionConcept))]
        public virtual ICollection<NoteNlp> NoteNlpSectionConcepts { get; set; }

        [InverseProperty(nameof(Note.NoteClassConcept))]
        public virtual ICollection<Note> NoteNoteClassConcepts { get; set; }

        [InverseProperty(nameof(Note.NoteTypeConcept))]
        public virtual ICollection<Note> NoteNoteTypeConcepts { get; set; }

        [InverseProperty(nameof(Observation.ObservationConcept))]
        public virtual ICollection<Observation> ObservationObservationConcepts { get; set; }

        [InverseProperty(nameof(Observation.ObservationSourceConcept))]
        public virtual ICollection<Observation> ObservationObservationSourceConcepts { get; set; }

        [InverseProperty(nameof(Observation.ObservationTypeConcept))]
        public virtual ICollection<Observation> ObservationObservationTypeConcepts { get; set; }

        [InverseProperty(nameof(ObservationPeriod.PeriodTypeConcept))]
        public virtual ICollection<ObservationPeriod> ObservationPeriods { get; set; }

        [InverseProperty(nameof(Observation.QualifierConcept))]
        public virtual ICollection<Observation> ObservationQualifierConcepts { get; set; }

        [InverseProperty(nameof(Observation.UnitConcept))]
        public virtual ICollection<Observation> ObservationUnitConcepts { get; set; }

        [InverseProperty(nameof(Observation.ValueAsConcept))]
        public virtual ICollection<Observation> ObservationValueAsConcepts { get; set; }

        [InverseProperty(nameof(Person.EthnicityConcept))]
        public virtual ICollection<Person> PersonEthnicityConcepts { get; set; }

        [InverseProperty(nameof(Person.EthnicitySourceConcept))]
        public virtual ICollection<Person> PersonEthnicitySourceConcepts { get; set; }

        [InverseProperty(nameof(Person.GenderConcept))]
        public virtual ICollection<Person> PersonGenderConcepts { get; set; }

        [InverseProperty(nameof(Person.GenderSourceConcept))]
        public virtual ICollection<Person> PersonGenderSourceConcepts { get; set; }

        [InverseProperty(nameof(Person.RaceConcept))]
        public virtual ICollection<Person> PersonRaceConcepts { get; set; }

        [InverseProperty(nameof(Person.RaceSourceConcept))]
        public virtual ICollection<Person> PersonRaceSourceConcepts { get; set; }

        [InverseProperty(nameof(ProcedureOccurrence.ModifierConcept))]
        public virtual ICollection<ProcedureOccurrence> ProcedureOccurrenceModifierConcepts { get; set; }

        [InverseProperty(nameof(ProcedureOccurrence.ProcedureConcept))]
        public virtual ICollection<ProcedureOccurrence> ProcedureOccurrenceProcedureConcepts { get; set; }

        [InverseProperty(nameof(ProcedureOccurrence.ProcedureSourceConcept))]
        public virtual ICollection<ProcedureOccurrence> ProcedureOccurrenceProcedureSourceConcepts { get; set; }

        [InverseProperty(nameof(ProcedureOccurrence.ProcedureTypeConcept))]
        public virtual ICollection<ProcedureOccurrence> ProcedureOccurrenceProcedureTypeConcepts { get; set; }

        [InverseProperty(nameof(Provider.GenderConcept))]
        public virtual ICollection<Provider> ProviderGenderConcepts { get; set; }

        [InverseProperty(nameof(Provider.GenderSourceConcept))]
        public virtual ICollection<Provider> ProviderGenderSourceConcepts { get; set; }

        [InverseProperty(nameof(Provider.SpecialtyConcept))]
        public virtual ICollection<Provider> ProviderSpecialtyConcepts { get; set; }

        [InverseProperty(nameof(Provider.SpecialtySourceConcept))]
        public virtual ICollection<Provider> ProviderSpecialtySourceConcepts { get; set; }

        [InverseProperty(nameof(Relationship.RelationshipConcept))]
        public virtual ICollection<Relationship> Relationships { get; set; }

        [InverseProperty(nameof(SourceToConceptMap.TargetConcept))]
        public virtual ICollection<SourceToConceptMap> SourceToConceptMaps { get; set; }

        [InverseProperty(nameof(Speciman.AnatomicSiteConcept))]
        public virtual ICollection<Speciman> SpecimanAnatomicSiteConcepts { get; set; }

        [InverseProperty(nameof(Speciman.DiseaseStatusConcept))]
        public virtual ICollection<Speciman> SpecimanDiseaseStatusConcepts { get; set; }

        [InverseProperty(nameof(Speciman.SpecimenConcept))]
        public virtual ICollection<Speciman> SpecimanSpecimenConcepts { get; set; }

        [InverseProperty(nameof(Speciman.SpecimenTypeConcept))]
        public virtual ICollection<Speciman> SpecimanSpecimenTypeConcepts { get; set; }

        [InverseProperty(nameof(Speciman.UnitConcept))]
        public virtual ICollection<Speciman> SpecimanUnitConcepts { get; set; }

        [InverseProperty(nameof(VisitOccurrence.AdmittingSourceConcept))]
        public virtual ICollection<VisitOccurrence> VisitOccurrenceAdmittingSourceConcepts { get; set; }

        [InverseProperty(nameof(VisitOccurrence.DischargeToConcept))]
        public virtual ICollection<VisitOccurrence> VisitOccurrenceDischargeToConcepts { get; set; }

        [InverseProperty(nameof(VisitOccurrence.VisitConcept))]
        public virtual ICollection<VisitOccurrence> VisitOccurrenceVisitConcepts { get; set; }

        [InverseProperty(nameof(VisitOccurrence.VisitSourceConcept))]
        public virtual ICollection<VisitOccurrence> VisitOccurrenceVisitSourceConcepts { get; set; }

        [InverseProperty(nameof(VisitOccurrence.VisitTypeConcept))]
        public virtual ICollection<VisitOccurrence> VisitOccurrenceVisitTypeConcepts { get; set; }

        [InverseProperty("VocabularyConcept")]
        public virtual ICollection<Vocabulary> Vocabularies { get; set; }
    }
}