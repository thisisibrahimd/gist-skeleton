using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("death")]
    [Index(nameof(PersonId), Name = "idx_death_person_id")]
    public class Death : BaseEntity, IAggregateRoot
    {
        [Key]
        [Column("person_id")]
        public int PersonId { get; set; }

        [Column("death_date", TypeName = "date")]
        public DateTime DeathDate { get; set; }

        [Column("death_datetime")]
        public DateTime? DeathDatetime { get; set; }

        [Column("death_type_concept_id")]
        public int DeathTypeConceptId { get; set; }

        [Column("cause_concept_id")]
        public int? CauseConceptId { get; set; }

        [Column("cause_source_value")]
        [StringLength(50)]
        public string CauseSourceValue { get; set; }

        [Column("cause_source_concept_id")]
        public int? CauseSourceConceptId { get; set; }

        [ForeignKey(nameof(CauseConceptId))]
        [InverseProperty(nameof(Concept.DeathCauseConcepts))]
        public virtual Concept CauseConcept { get; set; }

        [ForeignKey(nameof(CauseSourceConceptId))]
        [InverseProperty(nameof(Concept.DeathCauseSourceConcepts))]
        public virtual Concept CauseSourceConcept { get; set; }

        [ForeignKey(nameof(DeathTypeConceptId))]
        [InverseProperty(nameof(Concept.DeathDeathTypeConcepts))]
        public virtual Concept DeathTypeConcept { get; set; }

        [ForeignKey(nameof(PersonId))]
        [InverseProperty("Death")]
        public virtual Person Person { get; set; }
    }
}