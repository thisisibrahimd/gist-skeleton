using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("drug_era")]
    [Index(nameof(DrugConceptId), Name = "idx_drug_era_concept_id")]
    [Index(nameof(PersonId), Name = "idx_drug_era_person_id")]
    public class DrugEra : BaseEntity, IAggregateRoot
    {
        [Key]
        [Column("drug_era_id")]
        public int DrugEraId { get; set; }

        [Column("person_id")]
        public int PersonId { get; set; }

        [Column("drug_concept_id")]
        public int DrugConceptId { get; set; }

        [Column("drug_era_start_date", TypeName = "date")]
        public DateTime DrugEraStartDate { get; set; }

        [Column("drug_era_end_date", TypeName = "date")]
        public DateTime DrugEraEndDate { get; set; }

        [Column("drug_exposure_count")]
        public int? DrugExposureCount { get; set; }

        [Column("gap_days")]
        public int? GapDays { get; set; }

        [ForeignKey(nameof(DrugConceptId))]
        [InverseProperty(nameof(Concept.DrugEras))]
        public virtual Concept DrugConcept { get; set; }

        [ForeignKey(nameof(PersonId))]
        [InverseProperty("DrugEras")]
        public virtual Person Person { get; set; }
    }
}