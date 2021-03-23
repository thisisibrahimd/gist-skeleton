using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("drug_exposure")]
    [Index(nameof(DrugConceptId), Name = "idx_drug_concept_id")]
    [Index(nameof(PersonId), Name = "idx_drug_person_id")]
    [Index(nameof(VisitOccurrenceId), Name = "idx_drug_visit_id")]
    public class DrugExposure : BaseEntity, IAggregateRoot
    {
        [Key]
        [Column("drug_exposure_id")]
        public int DrugExposureId { get; set; }

        [Column("person_id")]
        public int PersonId { get; set; }

        [Column("drug_concept_id")]
        public int DrugConceptId { get; set; }

        [Column("drug_exposure_start_date", TypeName = "date")]
        public DateTime DrugExposureStartDate { get; set; }

        [Column("drug_exposure_start_datetime")]
        public DateTime DrugExposureStartDatetime { get; set; }

        [Column("drug_exposure_end_date", TypeName = "date")]
        public DateTime DrugExposureEndDate { get; set; }

        [Column("drug_exposure_end_datetime")]
        public DateTime? DrugExposureEndDatetime { get; set; }

        [Column("verbatim_end_date", TypeName = "date")]
        public DateTime? VerbatimEndDate { get; set; }

        [Column("drug_type_concept_id")]
        public int DrugTypeConceptId { get; set; }

        [Column("stop_reason")]
        [StringLength(20)]
        public string StopReason { get; set; }

        [Column("refills")]
        public int? Refills { get; set; }

        [Column("quantity")]
        public decimal? Quantity { get; set; }

        [Column("days_supply")]
        public int? DaysSupply { get; set; }

        [Column("sig")]
        public string Sig { get; set; }

        [Column("route_concept_id")]
        public int? RouteConceptId { get; set; }

        [Column("lot_number")]
        [StringLength(50)]
        public string LotNumber { get; set; }

        [Column("provider_id")]
        public int? ProviderId { get; set; }

        [Column("visit_occurrence_id")]
        public int? VisitOccurrenceId { get; set; }

        [Column("drug_source_value")]
        [StringLength(50)]
        public string DrugSourceValue { get; set; }

        [Column("drug_source_concept_id")]
        public int? DrugSourceConceptId { get; set; }

        [Column("route_source_value")]
        [StringLength(50)]
        public string RouteSourceValue { get; set; }

        [Column("dose_unit_source_value")]
        [StringLength(50)]
        public string DoseUnitSourceValue { get; set; }

        [ForeignKey(nameof(DrugConceptId))]
        [InverseProperty(nameof(Concept.DrugExposureDrugConcepts))]
        public virtual Concept DrugConcept { get; set; }

        [ForeignKey(nameof(DrugSourceConceptId))]
        [InverseProperty(nameof(Concept.DrugExposureDrugSourceConcepts))]
        public virtual Concept DrugSourceConcept { get; set; }

        [ForeignKey(nameof(DrugTypeConceptId))]
        [InverseProperty(nameof(Concept.DrugExposureDrugTypeConcepts))]
        public virtual Concept DrugTypeConcept { get; set; }

        [ForeignKey(nameof(PersonId))]
        [InverseProperty("DrugExposures")]
        public virtual Person Person { get; set; }

        [ForeignKey(nameof(ProviderId))]
        [InverseProperty("DrugExposures")]
        public virtual Provider Provider { get; set; }

        [ForeignKey(nameof(RouteConceptId))]
        [InverseProperty(nameof(Concept.DrugExposureRouteConcepts))]
        public virtual Concept RouteConcept { get; set; }

        [ForeignKey(nameof(VisitOccurrenceId))]
        [InverseProperty("DrugExposures")]
        public virtual VisitOccurrence VisitOccurrence { get; set; }
    }
}