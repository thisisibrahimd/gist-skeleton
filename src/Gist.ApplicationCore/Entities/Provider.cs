using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("provider")]
    public class Provider : BaseEntity, IAggregateRoot
    {
        public Provider()
        {
            ConditionOccurrences = new HashSet<ConditionOccurrence>();
            DeviceExposures = new HashSet<DeviceExposure>();
            DrugExposures = new HashSet<DrugExposure>();
            Measurements = new HashSet<Measurement>();
            Notes = new HashSet<Note>();
            Observations = new HashSet<Observation>();
            People = new HashSet<Person>();
            ProcedureOccurrences = new HashSet<ProcedureOccurrence>();
            VisitOccurrences = new HashSet<VisitOccurrence>();
        }

        [Key]
        [Column("provider_id")]
        public int ProviderId { get; set; }

        [Column("provider_name")]
        [StringLength(255)]
        public string ProviderName { get; set; }

        [Column("npi")]
        [StringLength(20)]
        public string Npi { get; set; }

        [Column("dea")]
        [StringLength(20)]
        public string Dea { get; set; }

        [Column("specialty_concept_id")]
        public int? SpecialtyConceptId { get; set; }

        [Column("care_site_id")]
        public int? CareSiteId { get; set; }

        [Column("year_of_birth")]
        public int? YearOfBirth { get; set; }

        [Column("gender_concept_id")]
        public int? GenderConceptId { get; set; }

        [Column("provider_source_value")]
        [StringLength(50)]
        public string ProviderSourceValue { get; set; }

        [Column("specialty_source_value")]
        [StringLength(50)]
        public string SpecialtySourceValue { get; set; }

        [Column("specialty_source_concept_id")]
        public int? SpecialtySourceConceptId { get; set; }

        [Column("gender_source_value")]
        [StringLength(50)]
        public string GenderSourceValue { get; set; }

        [Column("gender_source_concept_id")]
        public int? GenderSourceConceptId { get; set; }

        [ForeignKey(nameof(CareSiteId))]
        [InverseProperty("Providers")]
        public virtual CareSite CareSite { get; set; }

        [ForeignKey(nameof(GenderConceptId))]
        [InverseProperty(nameof(Concept.ProviderGenderConcepts))]
        public virtual Concept GenderConcept { get; set; }

        [ForeignKey(nameof(GenderSourceConceptId))]
        [InverseProperty(nameof(Concept.ProviderGenderSourceConcepts))]
        public virtual Concept GenderSourceConcept { get; set; }

        [ForeignKey(nameof(SpecialtyConceptId))]
        [InverseProperty(nameof(Concept.ProviderSpecialtyConcepts))]
        public virtual Concept SpecialtyConcept { get; set; }

        [ForeignKey(nameof(SpecialtySourceConceptId))]
        [InverseProperty(nameof(Concept.ProviderSpecialtySourceConcepts))]
        public virtual Concept SpecialtySourceConcept { get; set; }

        [InverseProperty(nameof(ConditionOccurrence.Provider))]
        public virtual ICollection<ConditionOccurrence> ConditionOccurrences { get; set; }

        [InverseProperty(nameof(DeviceExposure.Provider))]
        public virtual ICollection<DeviceExposure> DeviceExposures { get; set; }

        [InverseProperty(nameof(DrugExposure.Provider))]
        public virtual ICollection<DrugExposure> DrugExposures { get; set; }

        [InverseProperty(nameof(Measurement.Provider))]
        public virtual ICollection<Measurement> Measurements { get; set; }

        [InverseProperty(nameof(Note.Provider))]
        public virtual ICollection<Note> Notes { get; set; }

        [InverseProperty(nameof(Observation.Provider))]
        public virtual ICollection<Observation> Observations { get; set; }

        [InverseProperty(nameof(Person.Provider))]
        public virtual ICollection<Person> People { get; set; }

        [InverseProperty(nameof(ProcedureOccurrence.Provider))]
        public virtual ICollection<ProcedureOccurrence> ProcedureOccurrences { get; set; }

        [InverseProperty(nameof(VisitOccurrence.Provider))]
        public virtual ICollection<VisitOccurrence> VisitOccurrences { get; set; }
    }
}