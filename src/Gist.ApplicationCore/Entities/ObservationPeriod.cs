using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("observation_period")]
    [Index(nameof(PersonId), Name = "idx_observation_period_id")]
    public class ObservationPeriod : BaseEntity, IAggregateRoot
    {
        [Key]
        [Column("observation_period_id")]
        public int ObservationPeriodId { get; set; }

        [Column("person_id")]
        public int PersonId { get; set; }

        [Column("observation_period_start_date", TypeName = "date")]
        public DateTime ObservationPeriodStartDate { get; set; }

        [Column("observation_period_end_date", TypeName = "date")]
        public DateTime ObservationPeriodEndDate { get; set; }

        [Column("period_type_concept_id")]
        public int PeriodTypeConceptId { get; set; }

        [ForeignKey(nameof(PeriodTypeConceptId))]
        [InverseProperty(nameof(Concept.ObservationPeriods))]
        public virtual Concept PeriodTypeConcept { get; set; }

        [ForeignKey(nameof(PersonId))]
        [InverseProperty("ObservationPeriods")]
        public virtual Person Person { get; set; }
    }
}