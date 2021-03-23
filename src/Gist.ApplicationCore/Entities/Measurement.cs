using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("measurement")]
    [Index(nameof(MeasurementConceptId), Name = "idx_measurement_concept_id")]
    [Index(nameof(PersonId), Name = "idx_measurement_person_id")]
    [Index(nameof(VisitOccurrenceId), Name = "idx_measurement_visit_id")]
    public class Measurement : BaseEntity, IAggregateRoot
    {
        [Key]
        [Column("measurement_id")]
        public int MeasurementId { get; set; }

        [Column("person_id")]
        public int PersonId { get; set; }

        [Column("measurement_concept_id")]
        public int MeasurementConceptId { get; set; }

        [Column("measurement_date", TypeName = "date")]
        public DateTime MeasurementDate { get; set; }

        [Column("measurement_datetime")]
        public DateTime? MeasurementDatetime { get; set; }

        [Column("measurement_type_concept_id")]
        public int MeasurementTypeConceptId { get; set; }

        [Column("operator_concept_id")]
        public int? OperatorConceptId { get; set; }

        [Column("value_as_number")]
        public decimal? ValueAsNumber { get; set; }

        [Column("value_as_concept_id")]
        public int? ValueAsConceptId { get; set; }

        [Column("unit_concept_id")]
        public int? UnitConceptId { get; set; }

        [Column("range_low")]
        public decimal? RangeLow { get; set; }

        [Column("range_high")]
        public decimal? RangeHigh { get; set; }

        [Column("provider_id")]
        public int? ProviderId { get; set; }

        [Column("visit_occurrence_id")]
        public int? VisitOccurrenceId { get; set; }

        [Column("measurement_source_value")]
        [StringLength(50)]
        public string MeasurementSourceValue { get; set; }

        [Column("measurement_source_concept_id")]
        public int? MeasurementSourceConceptId { get; set; }

        [Column("unit_source_value")]
        [StringLength(50)]
        public string UnitSourceValue { get; set; }

        [Column("value_source_value")]
        [StringLength(50)]
        public string ValueSourceValue { get; set; }

        [ForeignKey(nameof(MeasurementConceptId))]
        [InverseProperty(nameof(Concept.MeasurementMeasurementConcepts))]
        public virtual Concept MeasurementConcept { get; set; }

        [ForeignKey(nameof(MeasurementSourceConceptId))]
        [InverseProperty(nameof(Concept.MeasurementMeasurementSourceConcepts))]
        public virtual Concept MeasurementSourceConcept { get; set; }

        [ForeignKey(nameof(MeasurementTypeConceptId))]
        [InverseProperty(nameof(Concept.MeasurementMeasurementTypeConcepts))]
        public virtual Concept MeasurementTypeConcept { get; set; }

        [ForeignKey(nameof(OperatorConceptId))]
        [InverseProperty(nameof(Concept.MeasurementOperatorConcepts))]
        public virtual Concept OperatorConcept { get; set; }

        [ForeignKey(nameof(PersonId))]
        [InverseProperty("Measurements")]
        public virtual Person Person { get; set; }

        [ForeignKey(nameof(ProviderId))]
        [InverseProperty("Measurements")]
        public virtual Provider Provider { get; set; }

        [ForeignKey(nameof(UnitConceptId))]
        [InverseProperty(nameof(Concept.MeasurementUnitConcepts))]
        public virtual Concept UnitConcept { get; set; }

        [ForeignKey(nameof(ValueAsConceptId))]
        [InverseProperty(nameof(Concept.MeasurementValueAsConcepts))]
        public virtual Concept ValueAsConcept { get; set; }

        [ForeignKey(nameof(VisitOccurrenceId))]
        [InverseProperty("Measurements")]
        public virtual VisitOccurrence VisitOccurrence { get; set; }
    }
}