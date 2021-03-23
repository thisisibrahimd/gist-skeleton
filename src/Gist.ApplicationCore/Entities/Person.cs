using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("person")]
    [Index(nameof(PersonId), Name = "idx_person_id", IsUnique = true)]
    public class Person : BaseEntity, IAggregateRoot
    {
        public Person()
        {
            ConditionEras = new HashSet<ConditionEra>();
            ConditionOccurrences = new HashSet<ConditionOccurrence>();
            DeviceExposures = new HashSet<DeviceExposure>();
            DoseEras = new HashSet<DoseEra>();
            DrugEras = new HashSet<DrugEra>();
            DrugExposures = new HashSet<DrugExposure>();
            Measurements = new HashSet<Measurement>();
            Notes = new HashSet<Note>();
            ObservationPeriods = new HashSet<ObservationPeriod>();
            Observations = new HashSet<Observation>();
            PayerPlanPeriods = new HashSet<PayerPlanPeriod>();
            ProcedureOccurrences = new HashSet<ProcedureOccurrence>();
            Specimen = new HashSet<Speciman>();
            VisitOccurrences = new HashSet<VisitOccurrence>();
        }

        [Key]
        [Column("person_id")]
        public int PersonId { get; set; }

        [Column("gender_concept_id")]
        public int GenderConceptId { get; set; }

        [Column("year_of_birth")]
        public int YearOfBirth { get; set; }

        [Column("month_of_birth")]
        public int? MonthOfBirth { get; set; }

        [Column("day_of_birth")]
        public int? DayOfBirth { get; set; }

        [Column("birth_datetime")]
        public DateTime? BirthDatetime { get; set; }

        [Column("race_concept_id")]
        public int RaceConceptId { get; set; }

        [Column("ethnicity_concept_id")]
        public int EthnicityConceptId { get; set; }

        [Column("location_id")]
        public int? LocationId { get; set; }

        [Column("provider_id")]
        public int? ProviderId { get; set; }

        [Column("care_site_id")]
        public int? CareSiteId { get; set; }

        [Column("person_source_value")]
        [StringLength(50)]
        public string PersonSourceValue { get; set; }

        [Column("gender_source_value")]
        [StringLength(50)]
        public string GenderSourceValue { get; set; }

        [Column("gender_source_concept_id")]
        public int? GenderSourceConceptId { get; set; }

        [Column("race_source_value")]
        [StringLength(50)]
        public string RaceSourceValue { get; set; }

        [Column("race_source_concept_id")]
        public int? RaceSourceConceptId { get; set; }

        [Column("ethnicity_source_value")]
        [StringLength(50)]
        public string EthnicitySourceValue { get; set; }

        [Column("ethnicity_source_concept_id")]
        public int? EthnicitySourceConceptId { get; set; }

        [ForeignKey(nameof(CareSiteId))]
        [InverseProperty("People")]
        public virtual CareSite CareSite { get; set; }

        [ForeignKey(nameof(EthnicityConceptId))]
        [InverseProperty(nameof(Concept.PersonEthnicityConcepts))]
        public virtual Concept EthnicityConcept { get; set; }

        [ForeignKey(nameof(EthnicitySourceConceptId))]
        [InverseProperty(nameof(Concept.PersonEthnicitySourceConcepts))]
        public virtual Concept EthnicitySourceConcept { get; set; }

        [ForeignKey(nameof(GenderConceptId))]
        [InverseProperty(nameof(Concept.PersonGenderConcepts))]
        public virtual Concept GenderConcept { get; set; }

        [ForeignKey(nameof(GenderSourceConceptId))]
        [InverseProperty(nameof(Concept.PersonGenderSourceConcepts))]
        public virtual Concept GenderSourceConcept { get; set; }

        [ForeignKey(nameof(LocationId))]
        [InverseProperty("People")]
        public virtual Location Location { get; set; }

        [ForeignKey(nameof(ProviderId))]
        [InverseProperty("People")]
        public virtual Provider Provider { get; set; }

        [ForeignKey(nameof(RaceConceptId))]
        [InverseProperty(nameof(Concept.PersonRaceConcepts))]
        public virtual Concept RaceConcept { get; set; }

        [ForeignKey(nameof(RaceSourceConceptId))]
        [InverseProperty(nameof(Concept.PersonRaceSourceConcepts))]
        public virtual Concept RaceSourceConcept { get; set; }

        [InverseProperty("Person")] public virtual Death Death { get; set; }

        [InverseProperty(nameof(ConditionEra.Person))]
        public virtual ICollection<ConditionEra> ConditionEras { get; set; }

        [InverseProperty(nameof(ConditionOccurrence.Person))]
        public virtual ICollection<ConditionOccurrence> ConditionOccurrences { get; set; }

        [InverseProperty(nameof(DeviceExposure.Person))]
        public virtual ICollection<DeviceExposure> DeviceExposures { get; set; }

        [InverseProperty(nameof(DoseEra.Person))]
        public virtual ICollection<DoseEra> DoseEras { get; set; }

        [InverseProperty(nameof(DrugEra.Person))]
        public virtual ICollection<DrugEra> DrugEras { get; set; }

        [InverseProperty(nameof(DrugExposure.Person))]
        public virtual ICollection<DrugExposure> DrugExposures { get; set; }

        [InverseProperty(nameof(Measurement.Person))]
        public virtual ICollection<Measurement> Measurements { get; set; }

        [InverseProperty(nameof(Note.Person))]
        public virtual ICollection<Note> Notes { get; set; }

        [InverseProperty(nameof(ObservationPeriod.Person))]
        public virtual ICollection<ObservationPeriod> ObservationPeriods { get; set; }

        [InverseProperty(nameof(Observation.Person))]
        public virtual ICollection<Observation> Observations { get; set; }

        [InverseProperty(nameof(PayerPlanPeriod.Person))]
        public virtual ICollection<PayerPlanPeriod> PayerPlanPeriods { get; set; }

        [InverseProperty(nameof(ProcedureOccurrence.Person))]
        public virtual ICollection<ProcedureOccurrence> ProcedureOccurrences { get; set; }

        [InverseProperty(nameof(Speciman.Person))]
        public virtual ICollection<Speciman> Specimen { get; set; }

        [InverseProperty(nameof(VisitOccurrence.Person))]
        public virtual ICollection<VisitOccurrence> VisitOccurrences { get; set; }
    }
}