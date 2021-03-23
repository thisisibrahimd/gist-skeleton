using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Keyless]
    [Table("fact_relationship")]
    [Index(nameof(DomainConceptId1), Name = "idx_fact_relationship_id_1")]
    [Index(nameof(DomainConceptId2), Name = "idx_fact_relationship_id_2")]
    [Index(nameof(RelationshipConceptId), Name = "idx_fact_relationship_id_3")]
    public class FactRelationship : BaseEntity, IAggregateRoot
    {
        [Column("domain_concept_id_1")]
        public int DomainConceptId1 { get; set; }

        [Column("fact_id_1")]
        public int FactId1 { get; set; }

        [Column("domain_concept_id_2")]
        public int DomainConceptId2 { get; set; }

        [Column("fact_id_2")]
        public int FactId2 { get; set; }

        [Column("relationship_concept_id")]
        public int RelationshipConceptId { get; set; }

        [ForeignKey(nameof(DomainConceptId1))]
        public virtual Concept DomainConceptId1Navigation { get; set; }

        [ForeignKey(nameof(DomainConceptId2))]
        public virtual Concept DomainConceptId2Navigation { get; set; }

        [ForeignKey(nameof(RelationshipConceptId))]
        public virtual Concept RelationshipConcept { get; set; }
    }
}