using Gist.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.Infrastructure.Data
{
    public class EhrContext : DbContext
    {
        public EhrContext()
        {
        }

        public EhrContext(DbContextOptions<EhrContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AttributeDefinition> AttributeDefinitions
        {
            get;
            set;
        }

        public virtual DbSet<CareSite> CareSites { get; set; }
        public virtual DbSet<CdmSource> CdmSources { get; set; }
        public virtual DbSet<Cohort> Cohorts { get; set; }
        public virtual DbSet<CohortAttribute> CohortAttributes { get; set; }
        public virtual DbSet<CohortDefinition> CohortDefinitions { get; set; }
        public virtual DbSet<Concept> Concepts { get; set; }
        public virtual DbSet<ConceptAncestor> ConceptAncestors { get; set; }
        public virtual DbSet<ConceptClass> ConceptClasses { get; set; }

        public virtual DbSet<ConceptRelationship> ConceptRelationships
        {
            get;
            set;
        }

        public virtual DbSet<ConceptSynonym> ConceptSynonyms { get; set; }
        public virtual DbSet<ConditionEra> ConditionEras { get; set; }

        public virtual DbSet<ConditionOccurrence> ConditionOccurrences
        {
            get;
            set;
        }

        public virtual DbSet<Cost> Costs { get; set; }
        public virtual DbSet<Death> Deaths { get; set; }
        public virtual DbSet<DeviceExposure> DeviceExposures { get; set; }
        public virtual DbSet<Domain> Domains { get; set; }
        public virtual DbSet<DoseEra> DoseEras { get; set; }
        public virtual DbSet<DrugEra> DrugEras { get; set; }
        public virtual DbSet<DrugExposure> DrugExposures { get; set; }
        public virtual DbSet<DrugStrength> DrugStrengths { get; set; }
        public virtual DbSet<FactRelationship> FactRelationships { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Measurement> Measurements { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<NoteNlp> NoteNlps { get; set; }
        public virtual DbSet<Observation> Observations { get; set; }
        public virtual DbSet<ObservationPeriod> ObservationPeriods { get; set; }
        public virtual DbSet<PayerPlanPeriod> PayerPlanPeriods { get; set; }
        public virtual DbSet<Person> People { get; set; }

        public virtual DbSet<ProcedureOccurrence> ProcedureOccurrences
        {
            get;
            set;
        }

        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<Relationship> Relationships { get; set; }

        public virtual DbSet<SourceToConceptMap> SourceToConceptMaps
        {
            get;
            set;
        }

        public virtual DbSet<Speciman> Specimen { get; set; }
        public virtual DbSet<VisitOccurrence> VisitOccurrences { get; set; }
        public virtual DbSet<Vocabulary> Vocabularies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "C");

            modelBuilder.Entity<AttributeDefinition>(entity =>
            {
                entity.Property(e => e.AttributeDefinitionId)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<CareSite>(entity =>
            {
                entity.Property(e => e.CareSiteId).ValueGeneratedNever();

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.CareSites)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("fpk_care_site_location");

                entity.HasOne(d => d.PlaceOfServiceConcept)
                    .WithMany(p => p.CareSites)
                    .HasForeignKey(d => d.PlaceOfServiceConceptId)
                    .HasConstraintName("fpk_care_site_place");
            });

            modelBuilder.Entity<Cohort>(entity =>
            {
                entity.HasKey(e => new
                    {
                        e.CohortDefinitionId, e.SubjectId, e.CohortStartDate,
                        e.CohortEndDate
                    })
                    .HasName("xpk_cohort");

                entity.HasOne(d => d.CohortDefinition)
                    .WithMany(p => p.Cohorts)
                    .HasForeignKey(d => d.CohortDefinitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_cohort_definition");
            });

            modelBuilder.Entity<CohortAttribute>(entity =>
            {
                entity.HasKey(e => new
                    {
                        e.CohortDefinitionId,
                        e.SubjectId,
                        e.CohortStartDate,
                        e.CohortEndDate,
                        e.AttributeDefinitionId
                    })
                    .HasName("xpk_cohort_attribute");

                entity.HasOne(d => d.AttributeDefinition)
                    .WithMany(p => p.CohortAttributes)
                    .HasForeignKey(d => d.AttributeDefinitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_ca_attribute_definition");

                entity.HasOne(d => d.CohortDefinition)
                    .WithMany(p => p.CohortAttributes)
                    .HasForeignKey(d => d.CohortDefinitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_ca_cohort_definition");

                entity.HasOne(d => d.ValueAsConcept)
                    .WithMany(p => p.CohortAttributes)
                    .HasForeignKey(d => d.ValueAsConceptId)
                    .HasConstraintName("fpk_ca_value");
            });

            modelBuilder.Entity<CohortDefinition>(entity =>
            {
                entity.Property(e => e.CohortDefinitionId)
                    .ValueGeneratedNever();

                entity.HasOne(d => d.DefinitionTypeConcept)
                    .WithMany(p => p.CohortDefinitions)
                    .HasForeignKey(d => d.DefinitionTypeConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_cohort_definition_concept");
            });

            modelBuilder.Entity<Concept>(entity =>
            {
                entity.Property(e => e.ConceptId).ValueGeneratedNever();

                entity.HasOne(d => d.ConceptClass)
                    .WithMany(p => p.Concepts)
                    .HasForeignKey(d => d.ConceptClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_concept_class");

                entity.HasOne(d => d.Domain)
                    .WithMany(p => p.Concepts)
                    .HasForeignKey(d => d.DomainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_concept_domain");

                entity.HasOne(d => d.Vocabulary)
                    .WithMany(p => p.Concepts)
                    .HasForeignKey(d => d.VocabularyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_concept_vocabulary");
            });

            modelBuilder.Entity<ConceptAncestor>(entity =>
            {
                entity.HasKey(e => new
                        {e.AncestorConceptId, e.DescendantConceptId})
                    .HasName("xpk_concept_ancestor");

                entity.HasOne(d => d.AncestorConcept)
                    .WithMany(p => p.ConceptAncestorAncestorConcepts)
                    .HasForeignKey(d => d.AncestorConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_concept_ancestor_concept_1");

                entity.HasOne(d => d.DescendantConcept)
                    .WithMany(p => p.ConceptAncestorDescendantConcepts)
                    .HasForeignKey(d => d.DescendantConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_concept_ancestor_concept_2");
            });

            modelBuilder.Entity<ConceptClass>(entity =>
            {
                entity.HasOne(d => d.ConceptClassConcept)
                    .WithMany(p => p.ConceptClasses)
                    .HasForeignKey(d => d.ConceptClassConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_concept_class_concept");
            });

            modelBuilder.Entity<ConceptRelationship>(entity =>
            {
                entity.HasKey(e => new
                        {e.ConceptId1, e.ConceptId2, e.RelationshipId})
                    .HasName("xpk_concept_relationship");

                entity.HasOne(d => d.ConceptId1Navigation)
                    .WithMany(p => p.ConceptRelationshipConceptId1Navigations)
                    .HasForeignKey(d => d.ConceptId1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_concept_relationship_c_1");

                entity.HasOne(d => d.ConceptId2Navigation)
                    .WithMany(p => p.ConceptRelationshipConceptId2Navigations)
                    .HasForeignKey(d => d.ConceptId2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_concept_relationship_c_2");

                entity.HasOne(d => d.Relationship)
                    .WithMany(p => p.ConceptRelationships)
                    .HasForeignKey(d => d.RelationshipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_concept_relationship_id");
            });

            modelBuilder.Entity<ConceptSynonym>(entity =>
            {
                entity.HasOne(d => d.Concept)
                    .WithMany()
                    .HasForeignKey(d => d.ConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_concept_synonym_concept");
            });

            modelBuilder.Entity<ConditionEra>(entity =>
            {
                entity.Property(e => e.ConditionEraId).ValueGeneratedNever();

                entity.HasOne(d => d.ConditionConcept)
                    .WithMany(p => p.ConditionEras)
                    .HasForeignKey(d => d.ConditionConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_condition_era_concept");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.ConditionEras)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_condition_era_person");
            });

            modelBuilder.Entity<ConditionOccurrence>(entity =>
            {
                entity.Property(e => e.ConditionOccurrenceId)
                    .ValueGeneratedNever();

                entity.HasOne(d => d.ConditionConcept)
                    .WithMany(p => p.ConditionOccurrenceConditionConcepts)
                    .HasForeignKey(d => d.ConditionConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_condition_concept");

                entity.HasOne(d => d.ConditionSourceConcept)
                    .WithMany(p => p.ConditionOccurrenceConditionSourceConcepts)
                    .HasForeignKey(d => d.ConditionSourceConceptId)
                    .HasConstraintName("fpk_condition_concept_s");

                entity.HasOne(d => d.ConditionStatusConcept)
                    .WithMany(p => p.ConditionOccurrenceConditionStatusConcepts)
                    .HasForeignKey(d => d.ConditionStatusConceptId)
                    .HasConstraintName("fpk_condition_status_concept");

                entity.HasOne(d => d.ConditionTypeConcept)
                    .WithMany(p => p.ConditionOccurrenceConditionTypeConcepts)
                    .HasForeignKey(d => d.ConditionTypeConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_condition_type_concept");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.ConditionOccurrences)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_condition_person");

                entity.HasOne(d => d.Provider)
                    .WithMany(p => p.ConditionOccurrences)
                    .HasForeignKey(d => d.ProviderId)
                    .HasConstraintName("fpk_condition_provider");

                entity.HasOne(d => d.VisitOccurrence)
                    .WithMany(p => p.ConditionOccurrences)
                    .HasForeignKey(d => d.VisitOccurrenceId)
                    .HasConstraintName("fpk_condition_visit");
            });

            modelBuilder.Entity<Cost>(entity =>
            {
                entity.Property(e => e.CostId).ValueGeneratedNever();

                entity.HasOne(d => d.CurrencyConcept)
                    .WithMany(p => p.CostCurrencyConcepts)
                    .HasForeignKey(d => d.CurrencyConceptId)
                    .HasConstraintName("fpk_visit_cost_currency");

                entity.HasOne(d => d.DrgConcept)
                    .WithMany(p => p.CostDrgConcepts)
                    .HasForeignKey(d => d.DrgConceptId)
                    .HasConstraintName("fpk_drg_concept");

                entity.HasOne(d => d.PayerPlanPeriod)
                    .WithMany(p => p.Costs)
                    .HasForeignKey(d => d.PayerPlanPeriodId)
                    .HasConstraintName("fpk_visit_cost_period");
            });

            modelBuilder.Entity<Death>(entity =>
            {
                entity.HasKey(e => e.PersonId)
                    .HasName("xpk_death");

                entity.Property(e => e.PersonId).ValueGeneratedNever();

                entity.HasOne(d => d.CauseConcept)
                    .WithMany(p => p.DeathCauseConcepts)
                    .HasForeignKey(d => d.CauseConceptId)
                    .HasConstraintName("fpk_death_cause_concept");

                entity.HasOne(d => d.CauseSourceConcept)
                    .WithMany(p => p.DeathCauseSourceConcepts)
                    .HasForeignKey(d => d.CauseSourceConceptId)
                    .HasConstraintName("fpk_death_cause_concept_s");

                entity.HasOne(d => d.DeathTypeConcept)
                    .WithMany(p => p.DeathDeathTypeConcepts)
                    .HasForeignKey(d => d.DeathTypeConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_death_type_concept");

                entity.HasOne(d => d.Person)
                    .WithOne(p => p.Death)
                    .HasForeignKey<Death>(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_death_person");
            });

            modelBuilder.Entity<DeviceExposure>(entity =>
            {
                entity.Property(e => e.DeviceExposureId).ValueGeneratedNever();

                entity.HasOne(d => d.DeviceConcept)
                    .WithMany(p => p.DeviceExposureDeviceConcepts)
                    .HasForeignKey(d => d.DeviceConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_device_concept");

                entity.HasOne(d => d.DeviceSourceConcept)
                    .WithMany(p => p.DeviceExposureDeviceSourceConcepts)
                    .HasForeignKey(d => d.DeviceSourceConceptId)
                    .HasConstraintName("fpk_device_concept_s");

                entity.HasOne(d => d.DeviceTypeConcept)
                    .WithMany(p => p.DeviceExposureDeviceTypeConcepts)
                    .HasForeignKey(d => d.DeviceTypeConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_device_type_concept");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.DeviceExposures)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_device_person");

                entity.HasOne(d => d.Provider)
                    .WithMany(p => p.DeviceExposures)
                    .HasForeignKey(d => d.ProviderId)
                    .HasConstraintName("fpk_device_provider");

                entity.HasOne(d => d.VisitOccurrence)
                    .WithMany(p => p.DeviceExposures)
                    .HasForeignKey(d => d.VisitOccurrenceId)
                    .HasConstraintName("fpk_device_visit");
            });

            modelBuilder.Entity<Domain>(entity =>
            {
                entity.HasOne(d => d.DomainConcept)
                    .WithMany(p => p.Domains)
                    .HasForeignKey(d => d.DomainConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_domain_concept");
            });

            modelBuilder.Entity<DoseEra>(entity =>
            {
                entity.Property(e => e.DoseEraId).ValueGeneratedNever();

                entity.HasOne(d => d.DrugConcept)
                    .WithMany(p => p.DoseEraDrugConcepts)
                    .HasForeignKey(d => d.DrugConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_dose_era_concept");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.DoseEras)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_dose_era_person");

                entity.HasOne(d => d.UnitConcept)
                    .WithMany(p => p.DoseEraUnitConcepts)
                    .HasForeignKey(d => d.UnitConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_dose_era_unit_concept");
            });

            modelBuilder.Entity<DrugEra>(entity =>
            {
                entity.Property(e => e.DrugEraId).ValueGeneratedNever();

                entity.HasOne(d => d.DrugConcept)
                    .WithMany(p => p.DrugEras)
                    .HasForeignKey(d => d.DrugConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_drug_era_concept");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.DrugEras)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_drug_era_person");
            });

            modelBuilder.Entity<DrugExposure>(entity =>
            {
                entity.Property(e => e.DrugExposureId).ValueGeneratedNever();

                entity.HasOne(d => d.DrugConcept)
                    .WithMany(p => p.DrugExposureDrugConcepts)
                    .HasForeignKey(d => d.DrugConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_drug_concept");

                entity.HasOne(d => d.DrugSourceConcept)
                    .WithMany(p => p.DrugExposureDrugSourceConcepts)
                    .HasForeignKey(d => d.DrugSourceConceptId)
                    .HasConstraintName("fpk_drug_concept_s");

                entity.HasOne(d => d.DrugTypeConcept)
                    .WithMany(p => p.DrugExposureDrugTypeConcepts)
                    .HasForeignKey(d => d.DrugTypeConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_drug_type_concept");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.DrugExposures)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_drug_person");

                entity.HasOne(d => d.Provider)
                    .WithMany(p => p.DrugExposures)
                    .HasForeignKey(d => d.ProviderId)
                    .HasConstraintName("fpk_drug_provider");

                entity.HasOne(d => d.RouteConcept)
                    .WithMany(p => p.DrugExposureRouteConcepts)
                    .HasForeignKey(d => d.RouteConceptId)
                    .HasConstraintName("fpk_drug_route_concept");

                entity.HasOne(d => d.VisitOccurrence)
                    .WithMany(p => p.DrugExposures)
                    .HasForeignKey(d => d.VisitOccurrenceId)
                    .HasConstraintName("fpk_drug_visit");
            });

            modelBuilder.Entity<DrugStrength>(entity =>
            {
                entity.HasKey(e => new {e.DrugConceptId, e.IngredientConceptId})
                    .HasName("xpk_drug_strength");

                entity.HasOne(d => d.AmountUnitConcept)
                    .WithMany(p => p.DrugStrengthAmountUnitConcepts)
                    .HasForeignKey(d => d.AmountUnitConceptId)
                    .HasConstraintName("fpk_drug_strength_unit_1");

                entity.HasOne(d => d.DenominatorUnitConcept)
                    .WithMany(p => p.DrugStrengthDenominatorUnitConcepts)
                    .HasForeignKey(d => d.DenominatorUnitConceptId)
                    .HasConstraintName("fpk_drug_strength_unit_3");

                entity.HasOne(d => d.DrugConcept)
                    .WithMany(p => p.DrugStrengthDrugConcepts)
                    .HasForeignKey(d => d.DrugConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_drug_strength_concept_1");

                entity.HasOne(d => d.IngredientConcept)
                    .WithMany(p => p.DrugStrengthIngredientConcepts)
                    .HasForeignKey(d => d.IngredientConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_drug_strength_concept_2");

                entity.HasOne(d => d.NumeratorUnitConcept)
                    .WithMany(p => p.DrugStrengthNumeratorUnitConcepts)
                    .HasForeignKey(d => d.NumeratorUnitConceptId)
                    .HasConstraintName("fpk_drug_strength_unit_2");
            });

            modelBuilder.Entity<FactRelationship>(entity =>
            {
                entity.HasOne(d => d.DomainConceptId1Navigation)
                    .WithMany()
                    .HasForeignKey(d => d.DomainConceptId1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_fact_domain_1");

                entity.HasOne(d => d.DomainConceptId2Navigation)
                    .WithMany()
                    .HasForeignKey(d => d.DomainConceptId2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_fact_domain_2");

                entity.HasOne(d => d.RelationshipConcept)
                    .WithMany()
                    .HasForeignKey(d => d.RelationshipConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_fact_relationship");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.LocationId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Measurement>(entity =>
            {
                entity.Property(e => e.MeasurementId).ValueGeneratedNever();

                entity.HasOne(d => d.MeasurementConcept)
                    .WithMany(p => p.MeasurementMeasurementConcepts)
                    .HasForeignKey(d => d.MeasurementConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_measurement_concept");

                entity.HasOne(d => d.MeasurementSourceConcept)
                    .WithMany(p => p.MeasurementMeasurementSourceConcepts)
                    .HasForeignKey(d => d.MeasurementSourceConceptId)
                    .HasConstraintName("fpk_measurement_concept_s");

                entity.HasOne(d => d.MeasurementTypeConcept)
                    .WithMany(p => p.MeasurementMeasurementTypeConcepts)
                    .HasForeignKey(d => d.MeasurementTypeConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_measurement_type_concept");

                entity.HasOne(d => d.OperatorConcept)
                    .WithMany(p => p.MeasurementOperatorConcepts)
                    .HasForeignKey(d => d.OperatorConceptId)
                    .HasConstraintName("fpk_measurement_operator");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Measurements)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_measurement_person");

                entity.HasOne(d => d.Provider)
                    .WithMany(p => p.Measurements)
                    .HasForeignKey(d => d.ProviderId)
                    .HasConstraintName("fpk_measurement_provider");

                entity.HasOne(d => d.UnitConcept)
                    .WithMany(p => p.MeasurementUnitConcepts)
                    .HasForeignKey(d => d.UnitConceptId)
                    .HasConstraintName("fpk_measurement_unit");

                entity.HasOne(d => d.ValueAsConcept)
                    .WithMany(p => p.MeasurementValueAsConcepts)
                    .HasForeignKey(d => d.ValueAsConceptId)
                    .HasConstraintName("fpk_measurement_value");

                entity.HasOne(d => d.VisitOccurrence)
                    .WithMany(p => p.Measurements)
                    .HasForeignKey(d => d.VisitOccurrenceId)
                    .HasConstraintName("fpk_measurement_visit");
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.Property(e => e.NoteId).ValueGeneratedNever();

                entity.HasOne(d => d.EncodingConcept)
                    .WithMany(p => p.NoteEncodingConcepts)
                    .HasForeignKey(d => d.EncodingConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_note_encoding_concept");

                entity.HasOne(d => d.LanguageConcept)
                    .WithMany(p => p.NoteLanguageConcepts)
                    .HasForeignKey(d => d.LanguageConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_language_concept");

                entity.HasOne(d => d.NoteClassConcept)
                    .WithMany(p => p.NoteNoteClassConcepts)
                    .HasForeignKey(d => d.NoteClassConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_note_class_concept");

                entity.HasOne(d => d.NoteTypeConcept)
                    .WithMany(p => p.NoteNoteTypeConcepts)
                    .HasForeignKey(d => d.NoteTypeConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_note_type_concept");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_note_person");

                entity.HasOne(d => d.Provider)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.ProviderId)
                    .HasConstraintName("fpk_note_provider");

                entity.HasOne(d => d.VisitOccurrence)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.VisitOccurrenceId)
                    .HasConstraintName("fpk_note_visit");
            });

            modelBuilder.Entity<NoteNlp>(entity =>
            {
                entity.Property(e => e.NoteNlpId).ValueGeneratedNever();

                entity.HasOne(d => d.Note)
                    .WithMany(p => p.NoteNlps)
                    .HasForeignKey(d => d.NoteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_note_nlp_note");

                entity.HasOne(d => d.NoteNlpConcept)
                    .WithMany(p => p.NoteNlpNoteNlpConcepts)
                    .HasForeignKey(d => d.NoteNlpConceptId)
                    .HasConstraintName("fpk_note_nlp_concept");

                entity.HasOne(d => d.SectionConcept)
                    .WithMany(p => p.NoteNlpSectionConcepts)
                    .HasForeignKey(d => d.SectionConceptId)
                    .HasConstraintName("fpk_note_nlp_section_concept");
            });

            modelBuilder.Entity<Observation>(entity =>
            {
                entity.Property(e => e.ObservationId).ValueGeneratedNever();

                entity.HasOne(d => d.ObservationConcept)
                    .WithMany(p => p.ObservationObservationConcepts)
                    .HasForeignKey(d => d.ObservationConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_observation_concept");

                entity.HasOne(d => d.ObservationSourceConcept)
                    .WithMany(p => p.ObservationObservationSourceConcepts)
                    .HasForeignKey(d => d.ObservationSourceConceptId)
                    .HasConstraintName("fpk_observation_concept_s");

                entity.HasOne(d => d.ObservationTypeConcept)
                    .WithMany(p => p.ObservationObservationTypeConcepts)
                    .HasForeignKey(d => d.ObservationTypeConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_observation_type_concept");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Observations)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_observation_person");

                entity.HasOne(d => d.Provider)
                    .WithMany(p => p.Observations)
                    .HasForeignKey(d => d.ProviderId)
                    .HasConstraintName("fpk_observation_provider");

                entity.HasOne(d => d.QualifierConcept)
                    .WithMany(p => p.ObservationQualifierConcepts)
                    .HasForeignKey(d => d.QualifierConceptId)
                    .HasConstraintName("fpk_observation_qualifier");

                entity.HasOne(d => d.UnitConcept)
                    .WithMany(p => p.ObservationUnitConcepts)
                    .HasForeignKey(d => d.UnitConceptId)
                    .HasConstraintName("fpk_observation_unit");

                entity.HasOne(d => d.ValueAsConcept)
                    .WithMany(p => p.ObservationValueAsConcepts)
                    .HasForeignKey(d => d.ValueAsConceptId)
                    .HasConstraintName("fpk_observation_value");

                entity.HasOne(d => d.VisitOccurrence)
                    .WithMany(p => p.Observations)
                    .HasForeignKey(d => d.VisitOccurrenceId)
                    .HasConstraintName("fpk_observation_visit");
            });

            modelBuilder.Entity<ObservationPeriod>(entity =>
            {
                entity.Property(e => e.ObservationPeriodId)
                    .ValueGeneratedNever();

                entity.HasOne(d => d.PeriodTypeConcept)
                    .WithMany(p => p.ObservationPeriods)
                    .HasForeignKey(d => d.PeriodTypeConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_observation_period_concept");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.ObservationPeriods)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_observation_period_person");
            });

            modelBuilder.Entity<PayerPlanPeriod>(entity =>
            {
                entity.Property(e => e.PayerPlanPeriodId).ValueGeneratedNever();

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PayerPlanPeriods)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_payer_plan_period");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.PersonId).ValueGeneratedNever();

                entity.HasOne(d => d.CareSite)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.CareSiteId)
                    .HasConstraintName("fpk_person_care_site");

                entity.HasOne(d => d.EthnicityConcept)
                    .WithMany(p => p.PersonEthnicityConcepts)
                    .HasForeignKey(d => d.EthnicityConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_person_ethnicity_concept");

                entity.HasOne(d => d.EthnicitySourceConcept)
                    .WithMany(p => p.PersonEthnicitySourceConcepts)
                    .HasForeignKey(d => d.EthnicitySourceConceptId)
                    .HasConstraintName("fpk_person_ethnicity_concept_s");

                entity.HasOne(d => d.GenderConcept)
                    .WithMany(p => p.PersonGenderConcepts)
                    .HasForeignKey(d => d.GenderConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_person_gender_concept");

                entity.HasOne(d => d.GenderSourceConcept)
                    .WithMany(p => p.PersonGenderSourceConcepts)
                    .HasForeignKey(d => d.GenderSourceConceptId)
                    .HasConstraintName("fpk_person_gender_concept_s");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("fpk_person_location");

                entity.HasOne(d => d.Provider)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.ProviderId)
                    .HasConstraintName("fpk_person_provider");

                entity.HasOne(d => d.RaceConcept)
                    .WithMany(p => p.PersonRaceConcepts)
                    .HasForeignKey(d => d.RaceConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_person_race_concept");

                entity.HasOne(d => d.RaceSourceConcept)
                    .WithMany(p => p.PersonRaceSourceConcepts)
                    .HasForeignKey(d => d.RaceSourceConceptId)
                    .HasConstraintName("fpk_person_race_concept_s");
            });

            modelBuilder.Entity<ProcedureOccurrence>(entity =>
            {
                entity.Property(e => e.ProcedureOccurrenceId)
                    .ValueGeneratedNever();

                entity.HasOne(d => d.ModifierConcept)
                    .WithMany(p => p.ProcedureOccurrenceModifierConcepts)
                    .HasForeignKey(d => d.ModifierConceptId)
                    .HasConstraintName("fpk_procedure_modifier");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.ProcedureOccurrences)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_procedure_person");

                entity.HasOne(d => d.ProcedureConcept)
                    .WithMany(p => p.ProcedureOccurrenceProcedureConcepts)
                    .HasForeignKey(d => d.ProcedureConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_procedure_concept");

                entity.HasOne(d => d.ProcedureSourceConcept)
                    .WithMany(p => p.ProcedureOccurrenceProcedureSourceConcepts)
                    .HasForeignKey(d => d.ProcedureSourceConceptId)
                    .HasConstraintName("fpk_procedure_concept_s");

                entity.HasOne(d => d.ProcedureTypeConcept)
                    .WithMany(p => p.ProcedureOccurrenceProcedureTypeConcepts)
                    .HasForeignKey(d => d.ProcedureTypeConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_procedure_type_concept");

                entity.HasOne(d => d.Provider)
                    .WithMany(p => p.ProcedureOccurrences)
                    .HasForeignKey(d => d.ProviderId)
                    .HasConstraintName("fpk_procedure_provider");

                entity.HasOne(d => d.VisitOccurrence)
                    .WithMany(p => p.ProcedureOccurrences)
                    .HasForeignKey(d => d.VisitOccurrenceId)
                    .HasConstraintName("fpk_procedure_visit");
            });

            modelBuilder.Entity<Provider>(entity =>
            {
                entity.Property(e => e.ProviderId).ValueGeneratedNever();

                entity.HasOne(d => d.CareSite)
                    .WithMany(p => p.Providers)
                    .HasForeignKey(d => d.CareSiteId)
                    .HasConstraintName("fpk_provider_care_site");

                entity.HasOne(d => d.GenderConcept)
                    .WithMany(p => p.ProviderGenderConcepts)
                    .HasForeignKey(d => d.GenderConceptId)
                    .HasConstraintName("fpk_provider_gender");

                entity.HasOne(d => d.GenderSourceConcept)
                    .WithMany(p => p.ProviderGenderSourceConcepts)
                    .HasForeignKey(d => d.GenderSourceConceptId)
                    .HasConstraintName("fpk_provider_gender_s");

                entity.HasOne(d => d.SpecialtyConcept)
                    .WithMany(p => p.ProviderSpecialtyConcepts)
                    .HasForeignKey(d => d.SpecialtyConceptId)
                    .HasConstraintName("fpk_provider_specialty");

                entity.HasOne(d => d.SpecialtySourceConcept)
                    .WithMany(p => p.ProviderSpecialtySourceConcepts)
                    .HasForeignKey(d => d.SpecialtySourceConceptId)
                    .HasConstraintName("fpk_provider_specialty_s");
            });

            modelBuilder.Entity<Relationship>(entity =>
            {
                entity.HasOne(d => d.RelationshipConcept)
                    .WithMany(p => p.Relationships)
                    .HasForeignKey(d => d.RelationshipConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_relationship_concept");

                entity.HasOne(d => d.ReverseRelationship)
                    .WithMany(p => p.InverseReverseRelationship)
                    .HasForeignKey(d => d.ReverseRelationshipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_relationship_reverse");
            });

            modelBuilder.Entity<SourceToConceptMap>(entity =>
            {
                entity.HasKey(e => new
                    {
                        e.SourceVocabularyId, e.TargetConceptId, e.SourceCode,
                        e.ValidEndDate
                    })
                    .HasName("xpk_source_to_concept_map");

                entity.HasOne(d => d.SourceVocabulary)
                    .WithMany(p => p.SourceToConceptMapSourceVocabularies)
                    .HasForeignKey(d => d.SourceVocabularyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_source_to_concept_map_v_1");

                entity.HasOne(d => d.TargetConcept)
                    .WithMany(p => p.SourceToConceptMaps)
                    .HasForeignKey(d => d.TargetConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_source_to_concept_map_c_1");

                entity.HasOne(d => d.TargetVocabulary)
                    .WithMany(p => p.SourceToConceptMapTargetVocabularies)
                    .HasForeignKey(d => d.TargetVocabularyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_source_to_concept_map_v_2");
            });

            modelBuilder.Entity<Speciman>(entity =>
            {
                entity.HasKey(e => e.SpecimenId)
                    .HasName("xpk_specimen");

                entity.Property(e => e.SpecimenId).ValueGeneratedNever();

                entity.HasOne(d => d.AnatomicSiteConcept)
                    .WithMany(p => p.SpecimanAnatomicSiteConcepts)
                    .HasForeignKey(d => d.AnatomicSiteConceptId)
                    .HasConstraintName("fpk_specimen_site_concept");

                entity.HasOne(d => d.DiseaseStatusConcept)
                    .WithMany(p => p.SpecimanDiseaseStatusConcepts)
                    .HasForeignKey(d => d.DiseaseStatusConceptId)
                    .HasConstraintName("fpk_specimen_status_concept");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Specimen)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_specimen_person");

                entity.HasOne(d => d.SpecimenConcept)
                    .WithMany(p => p.SpecimanSpecimenConcepts)
                    .HasForeignKey(d => d.SpecimenConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_specimen_concept");

                entity.HasOne(d => d.SpecimenTypeConcept)
                    .WithMany(p => p.SpecimanSpecimenTypeConcepts)
                    .HasForeignKey(d => d.SpecimenTypeConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_specimen_type_concept");

                entity.HasOne(d => d.UnitConcept)
                    .WithMany(p => p.SpecimanUnitConcepts)
                    .HasForeignKey(d => d.UnitConceptId)
                    .HasConstraintName("fpk_specimen_unit_concept");
            });

            modelBuilder.Entity<VisitOccurrence>(entity =>
            {
                entity.Property(e => e.VisitOccurrenceId).ValueGeneratedNever();

                entity.HasOne(d => d.AdmittingSourceConcept)
                    .WithMany(p => p.VisitOccurrenceAdmittingSourceConcepts)
                    .HasForeignKey(d => d.AdmittingSourceConceptId)
                    .HasConstraintName("fpk_visit_admitting_s");

                entity.HasOne(d => d.CareSite)
                    .WithMany(p => p.VisitOccurrences)
                    .HasForeignKey(d => d.CareSiteId)
                    .HasConstraintName("fpk_visit_care_site");

                entity.HasOne(d => d.DischargeToConcept)
                    .WithMany(p => p.VisitOccurrenceDischargeToConcepts)
                    .HasForeignKey(d => d.DischargeToConceptId)
                    .HasConstraintName("fpk_visit_discharge");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.VisitOccurrences)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_visit_person");

                entity.HasOne(d => d.Provider)
                    .WithMany(p => p.VisitOccurrences)
                    .HasForeignKey(d => d.ProviderId)
                    .HasConstraintName("fpk_visit_provider");

                entity.HasOne(d => d.VisitConcept)
                    .WithMany(p => p.VisitOccurrenceVisitConcepts)
                    .HasForeignKey(d => d.VisitConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_visit_concept");

                entity.HasOne(d => d.VisitSourceConcept)
                    .WithMany(p => p.VisitOccurrenceVisitSourceConcepts)
                    .HasForeignKey(d => d.VisitSourceConceptId)
                    .HasConstraintName("fpk_visit_concept_s");

                entity.HasOne(d => d.VisitTypeConcept)
                    .WithMany(p => p.VisitOccurrenceVisitTypeConcepts)
                    .HasForeignKey(d => d.VisitTypeConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_visit_type_concept");
            });

            modelBuilder.Entity<Vocabulary>(entity =>
            {
                entity.HasOne(d => d.VocabularyConcept)
                    .WithMany(p => p.Vocabularies)
                    .HasForeignKey(d => d.VocabularyConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fpk_vocabulary_concept");
            });
        }
    }
}