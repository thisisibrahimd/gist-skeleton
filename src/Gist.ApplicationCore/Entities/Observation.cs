using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("observation")]
    [Index(nameof(ObservationConceptId), Name = "idx_observation_concept_id")]
    [Index(nameof(PersonId), Name = "idx_observation_person_id")]
    [Index(nameof(VisitOccurrenceId), Name = "idx_observation_visit_id")]
    public class Observation : BaseEntity, IAggregateRoot
    {
        [Key]
        [Column("observation_id")]
        public int ObservationId { get; set; }

        [Column("person_id")]
        public int PersonId { get; set; }

        [Column("observation_concept_id")]
        public int ObservationConceptId { get; set; }

        [Column("observation_date", TypeName = "date")]
        public DateTime ObservationDate { get; set; }

        [Column("observation_datetime")]
        public DateTime? ObservationDatetime { get; set; }

        [Column("observation_type_concept_id")]
        public int ObservationTypeConceptId { get; set; }

        [Column("value_as_number")]
        public decimal? ValueAsNumber { get; set; }

        [Column("value_as_string")]
        [StringLength(60)]
        public string ValueAsString { get; set; }

        [Column("value_as_concept_id")]
        public int? ValueAsConceptId { get; set; }

        [Column("qualifier_concept_id")]
        public int? QualifierConceptId { get; set; }

        [Column("unit_concept_id")]
        public int? UnitConceptId { get; set; }

        [Column("provider_id")]
        public int? ProviderId { get; set; }

        [Column("visit_occurrence_id")]
        public int? VisitOccurrenceId { get; set; }

        [Column("observation_source_value")]
        [StringLength(50)]
        public string ObservationSourceValue { get; set; }

        [Column("observation_source_concept_id")]
        public int? ObservationSourceConceptId { get; set; }

        [Column("unit_source_value")]
        [StringLength(50)]
        public string UnitSourceValue { get; set; }

        [Column("qualifier_source_value")]
        [StringLength(50)]
        public string QualifierSourceValue { get; set; }

        [ForeignKey(nameof(ObservationConceptId))]
        [InverseProperty(nameof(Concept.ObservationObservationConcepts))]
        public virtual Concept ObservationConcept { get; set; }

        [ForeignKey(nameof(ObservationSourceConceptId))]
        [InverseProperty(nameof(Concept.ObservationObservationSourceConcepts))]
        public virtual Concept ObservationSourceConcept { get; set; }

        [ForeignKey(nameof(ObservationTypeConceptId))]
        [InverseProperty(nameof(Concept.ObservationObservationTypeConcepts))]
        public virtual Concept ObservationTypeConcept { get; set; }

        [ForeignKey(nameof(PersonId))]
        [InverseProperty("Observations")]
        public virtual Person Person { get; set; }

        [ForeignKey(nameof(ProviderId))]
        [InverseProperty("Observations")]
        public virtual Provider Provider { get; set; }

        [ForeignKey(nameof(QualifierConceptId))]
        [InverseProperty(nameof(Concept.ObservationQualifierConcepts))]
        public virtual Concept QualifierConcept { get; set; }

        [ForeignKey(nameof(UnitConceptId))]
        [InverseProperty(nameof(Concept.ObservationUnitConcepts))]
        public virtual Concept UnitConcept { get; set; }

        [ForeignKey(nameof(ValueAsConceptId))]
        [InverseProperty(nameof(Concept.ObservationValueAsConcepts))]
        public virtual Concept ValueAsConcept { get; set; }

        [ForeignKey(nameof(VisitOccurrenceId))]
        [InverseProperty("Observations")]
        public virtual VisitOccurrence VisitOccurrence { get; set; }
    }
}