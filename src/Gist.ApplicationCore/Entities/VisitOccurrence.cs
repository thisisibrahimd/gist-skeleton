using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("visit_occurrence")]
    [Index(nameof(VisitConceptId), Name = "idx_visit_concept_id")]
    [Index(nameof(PersonId), Name = "idx_visit_person_id")]
    public class VisitOccurrence : BaseEntity, IAggregateRoot
    {
        public VisitOccurrence()
        {
            ConditionOccurrences = new HashSet<ConditionOccurrence>();
            DeviceExposures = new HashSet<DeviceExposure>();
            DrugExposures = new HashSet<DrugExposure>();
            Measurements = new HashSet<Measurement>();
            Notes = new HashSet<Note>();
            Observations = new HashSet<Observation>();
            ProcedureOccurrences = new HashSet<ProcedureOccurrence>();
        }

        [Key]
        [Column("visit_occurrence_id")]
        public int VisitOccurrenceId { get; set; }

        [Column("person_id")]
        public int PersonId { get; set; }

        [Column("visit_concept_id")]
        public int VisitConceptId { get; set; }

        [Column("visit_start_date", TypeName = "date")]
        public DateTime VisitStartDate { get; set; }

        [Column("visit_start_datetime")]
        public DateTime? VisitStartDatetime { get; set; }

        [Column("visit_end_date", TypeName = "date")]
        public DateTime VisitEndDate { get; set; }

        [Column("visit_end_datetime")]
        public DateTime? VisitEndDatetime { get; set; }

        [Column("visit_type_concept_id")]
        public int VisitTypeConceptId { get; set; }

        [Column("provider_id")]
        public int? ProviderId { get; set; }

        [Column("care_site_id")]
        public int? CareSiteId { get; set; }

        [Column("visit_source_value")]
        [StringLength(50)]
        public string VisitSourceValue { get; set; }

        [Column("visit_source_concept_id")]
        public int? VisitSourceConceptId { get; set; }

        [Column("admitting_source_concept_id")]
        public int? AdmittingSourceConceptId { get; set; }

        [Column("admitting_source_value")]
        [StringLength(50)]
        public string AdmittingSourceValue { get; set; }

        [Column("discharge_to_concept_id")]
        public int? DischargeToConceptId { get; set; }

        [Column("discharge_to_source_value")]
        [StringLength(50)]
        public string DischargeToSourceValue { get; set; }

        [Column("preceding_visit_occurrence_id")]
        public int? PrecedingVisitOccurrenceId { get; set; }

        [ForeignKey(nameof(AdmittingSourceConceptId))]
        [InverseProperty(nameof(Concept.VisitOccurrenceAdmittingSourceConcepts))]
        public virtual Concept AdmittingSourceConcept { get; set; }

        [ForeignKey(nameof(CareSiteId))]
        [InverseProperty("VisitOccurrences")]
        public virtual CareSite CareSite { get; set; }

        [ForeignKey(nameof(DischargeToConceptId))]
        [InverseProperty(nameof(Concept.VisitOccurrenceDischargeToConcepts))]
        public virtual Concept DischargeToConcept { get; set; }

        [ForeignKey(nameof(PersonId))]
        [InverseProperty("VisitOccurrences")]
        public virtual Person Person { get; set; }

        [ForeignKey(nameof(ProviderId))]
        [InverseProperty("VisitOccurrences")]
        public virtual Provider Provider { get; set; }

        [ForeignKey(nameof(VisitConceptId))]
        [InverseProperty(nameof(Concept.VisitOccurrenceVisitConcepts))]
        public virtual Concept VisitConcept { get; set; }

        [ForeignKey(nameof(VisitSourceConceptId))]
        [InverseProperty(nameof(Concept.VisitOccurrenceVisitSourceConcepts))]
        public virtual Concept VisitSourceConcept { get; set; }

        [ForeignKey(nameof(VisitTypeConceptId))]
        [InverseProperty(nameof(Concept.VisitOccurrenceVisitTypeConcepts))]
        public virtual Concept VisitTypeConcept { get; set; }

        [InverseProperty(nameof(ConditionOccurrence.VisitOccurrence))]
        public virtual ICollection<ConditionOccurrence> ConditionOccurrences { get; set; }

        [InverseProperty(nameof(DeviceExposure.VisitOccurrence))]
        public virtual ICollection<DeviceExposure> DeviceExposures { get; set; }

        [InverseProperty(nameof(DrugExposure.VisitOccurrence))]
        public virtual ICollection<DrugExposure> DrugExposures { get; set; }

        [InverseProperty(nameof(Measurement.VisitOccurrence))]
        public virtual ICollection<Measurement> Measurements { get; set; }

        [InverseProperty(nameof(Note.VisitOccurrence))]
        public virtual ICollection<Note> Notes { get; set; }

        [InverseProperty(nameof(Observation.VisitOccurrence))]
        public virtual ICollection<Observation> Observations { get; set; }

        [InverseProperty(nameof(ProcedureOccurrence.VisitOccurrence))]
        public virtual ICollection<ProcedureOccurrence> ProcedureOccurrences { get; set; }
    }
}