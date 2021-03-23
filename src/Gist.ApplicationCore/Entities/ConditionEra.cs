using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("condition_era")]
    [Index(nameof(ConditionConceptId), Name = "idx_condition_era_concept_id")]
    [Index(nameof(PersonId), Name = "idx_condition_era_person_id")]
    public class ConditionEra : BaseEntity, IAggregateRoot
    {
        [Key]
        [Column("condition_era_id")]
        public int ConditionEraId { get; set; }

        [Column("person_id")]
        public int PersonId { get; set; }

        [Column("condition_concept_id")]
        public int ConditionConceptId { get; set; }

        [Column("condition_era_start_date", TypeName = "date")]
        public DateTime ConditionEraStartDate { get; set; }

        [Column("condition_era_end_date", TypeName = "date")]
        public DateTime ConditionEraEndDate { get; set; }

        [Column("condition_occurrence_count")]
        public int? ConditionOccurrenceCount { get; set; }

        [ForeignKey(nameof(ConditionConceptId))]
        [InverseProperty(nameof(Concept.ConditionEras))]
        public virtual Concept ConditionConcept { get; set; }

        [ForeignKey(nameof(PersonId))]
        [InverseProperty("ConditionEras")]
        public virtual Person Person { get; set; }
    }
}