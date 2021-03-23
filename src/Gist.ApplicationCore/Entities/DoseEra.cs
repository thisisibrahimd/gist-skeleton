using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("dose_era")]
    [Index(nameof(DrugConceptId), Name = "idx_dose_era_concept_id")]
    [Index(nameof(PersonId), Name = "idx_dose_era_person_id")]
    public class DoseEra : BaseEntity, IAggregateRoot
    {
        [Key]
        [Column("dose_era_id")]
        public int DoseEraId { get; set; }

        [Column("person_id")]
        public int PersonId { get; set; }

        [Column("drug_concept_id")]
        public int DrugConceptId { get; set; }

        [Column("unit_concept_id")]
        public int UnitConceptId { get; set; }

        [Column("dose_value")]
        public decimal DoseValue { get; set; }

        [Column("dose_era_start_date", TypeName = "date")]
        public DateTime DoseEraStartDate { get; set; }

        [Column("dose_era_end_date", TypeName = "date")]
        public DateTime DoseEraEndDate { get; set; }

        [ForeignKey(nameof(DrugConceptId))]
        [InverseProperty(nameof(Concept.DoseEraDrugConcepts))]
        public virtual Concept DrugConcept { get; set; }

        [ForeignKey(nameof(PersonId))]
        [InverseProperty("DoseEras")]
        public virtual Person Person { get; set; }

        [ForeignKey(nameof(UnitConceptId))]
        [InverseProperty(nameof(Concept.DoseEraUnitConcepts))]
        public virtual Concept UnitConcept { get; set; }
    }
}