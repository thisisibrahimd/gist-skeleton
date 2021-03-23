using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("condition_occurrence")]
    [Index(nameof(ConditionConceptId), Name = "idx_condition_concept_id")]
    [Index(nameof(PersonId), Name = "idx_condition_person_id")]
    [Index(nameof(VisitOccurrenceId), Name = "idx_condition_visit_id")]
    public class ConditionOccurrence : BaseEntity, IAggregateRoot
    {
        [Key]
        [Column("condition_occurrence_id")]
        public int ConditionOccurrenceId { get; set; }

        [Column("person_id")]
        public int PersonId { get; set; }

        [Column("condition_concept_id")]
        public int ConditionConceptId { get; set; }

        [Column("condition_start_date", TypeName = "date")]
        public DateTime ConditionStartDate { get; set; }

        [Column("condition_start_datetime")]
        public DateTime ConditionStartDatetime { get; set; }

        [Column("condition_end_date", TypeName = "date")]
        public DateTime? ConditionEndDate { get; set; }

        [Column("condition_end_datetime")]
        public DateTime? ConditionEndDatetime { get; set; }

        [Column("condition_type_concept_id")]
        public int ConditionTypeConceptId { get; set; }

        [Column("stop_reason")]
        [StringLength(20)]
        public string StopReason { get; set; }

        [Column("provider_id")]
        public int? ProviderId { get; set; }

        [Column("visit_occurrence_id")]
        public int? VisitOccurrenceId { get; set; }

        [Column("condition_source_value")]
        [StringLength(50)]
        public string ConditionSourceValue { get; set; }

        [Column("condition_source_concept_id")]
        public int? ConditionSourceConceptId { get; set; }

        [Column("condition_status_source_value")]
        [StringLength(50)]
        public string ConditionStatusSourceValue { get; set; }

        [Column("condition_status_concept_id")]
        public int? ConditionStatusConceptId { get; set; }

        [ForeignKey(nameof(ConditionConceptId))]
        [InverseProperty(nameof(Concept.ConditionOccurrenceConditionConcepts))]
        public virtual Concept ConditionConcept { get; set; }

        [ForeignKey(nameof(ConditionSourceConceptId))]
        [InverseProperty(nameof(Concept.ConditionOccurrenceConditionSourceConcepts))]
        public virtual Concept ConditionSourceConcept { get; set; }

        [ForeignKey(nameof(ConditionStatusConceptId))]
        [InverseProperty(nameof(Concept.ConditionOccurrenceConditionStatusConcepts))]
        public virtual Concept ConditionStatusConcept { get; set; }

        [ForeignKey(nameof(ConditionTypeConceptId))]
        [InverseProperty(nameof(Concept.ConditionOccurrenceConditionTypeConcepts))]
        public virtual Concept ConditionTypeConcept { get; set; }

        [ForeignKey(nameof(PersonId))]
        [InverseProperty("ConditionOccurrences")]
        public virtual Person Person { get; set; }

        [ForeignKey(nameof(ProviderId))]
        [InverseProperty("ConditionOccurrences")]
        public virtual Provider Provider { get; set; }

        [ForeignKey(nameof(VisitOccurrenceId))]
        [InverseProperty("ConditionOccurrences")]
        public virtual VisitOccurrence VisitOccurrence { get; set; }
    }
}