using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("procedure_occurrence")]
    [Index(nameof(ProcedureConceptId), Name = "idx_procedure_concept_id")]
    [Index(nameof(PersonId), Name = "idx_procedure_person_id")]
    [Index(nameof(VisitOccurrenceId), Name = "idx_procedure_visit_id")]
    public class ProcedureOccurrence : BaseEntity, IAggregateRoot
    {
        [Key]
        [Column("procedure_occurrence_id")]
        public int ProcedureOccurrenceId { get; set; }

        [Column("person_id")]
        public int PersonId { get; set; }

        [Column("procedure_concept_id")]
        public int ProcedureConceptId { get; set; }

        [Column("procedure_date", TypeName = "date")]
        public DateTime ProcedureDate { get; set; }

        [Column("procedure_datetime")]
        public DateTime ProcedureDatetime { get; set; }

        [Column("procedure_type_concept_id")]
        public int ProcedureTypeConceptId { get; set; }

        [Column("modifier_concept_id")]
        public int? ModifierConceptId { get; set; }

        [Column("quantity")]
        public int? Quantity { get; set; }

        [Column("provider_id")]
        public int? ProviderId { get; set; }

        [Column("visit_occurrence_id")]
        public int? VisitOccurrenceId { get; set; }

        [Column("procedure_source_value")]
        [StringLength(50)]
        public string ProcedureSourceValue { get; set; }

        [Column("procedure_source_concept_id")]
        public int? ProcedureSourceConceptId { get; set; }

        [Column("qualifier_source_value")]
        [StringLength(50)]
        public string QualifierSourceValue { get; set; }

        [ForeignKey(nameof(ModifierConceptId))]
        [InverseProperty(nameof(Concept.ProcedureOccurrenceModifierConcepts))]
        public virtual Concept ModifierConcept { get; set; }

        [ForeignKey(nameof(PersonId))]
        [InverseProperty("ProcedureOccurrences")]
        public virtual Person Person { get; set; }

        [ForeignKey(nameof(ProcedureConceptId))]
        [InverseProperty(nameof(Concept.ProcedureOccurrenceProcedureConcepts))]
        public virtual Concept ProcedureConcept { get; set; }

        [ForeignKey(nameof(ProcedureSourceConceptId))]
        [InverseProperty(nameof(Concept.ProcedureOccurrenceProcedureSourceConcepts))]
        public virtual Concept ProcedureSourceConcept { get; set; }

        [ForeignKey(nameof(ProcedureTypeConceptId))]
        [InverseProperty(nameof(Concept.ProcedureOccurrenceProcedureTypeConcepts))]
        public virtual Concept ProcedureTypeConcept { get; set; }

        [ForeignKey(nameof(ProviderId))]
        [InverseProperty("ProcedureOccurrences")]
        public virtual Provider Provider { get; set; }

        [ForeignKey(nameof(VisitOccurrenceId))]
        [InverseProperty("ProcedureOccurrences")]
        public virtual VisitOccurrence VisitOccurrence { get; set; }
    }
}