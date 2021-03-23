using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("concept_relationship")]
    [Index(nameof(ConceptId1), Name = "idx_concept_relationship_id_1")]
    [Index(nameof(ConceptId2), Name = "idx_concept_relationship_id_2")]
    [Index(nameof(RelationshipId), Name = "idx_concept_relationship_id_3")]
    public class ConceptRelationship : BaseEntity, IAggregateRoot
    {
        [Key]
        [Column("concept_id_1")]
        public int ConceptId1 { get; set; }

        [Key]
        [Column("concept_id_2")]
        public int ConceptId2 { get; set; }

        [Key]
        [Column("relationship_id")]
        [StringLength(20)]
        public string RelationshipId { get; set; }

        [Column("valid_start_date", TypeName = "date")]
        public DateTime ValidStartDate { get; set; }

        [Column("valid_end_date", TypeName = "date")]
        public DateTime ValidEndDate { get; set; }

        [Column("invalid_reason")]
        [StringLength(1)]
        public string InvalidReason { get; set; }

        [ForeignKey(nameof(ConceptId1))]
        [InverseProperty(nameof(Concept.ConceptRelationshipConceptId1Navigations))]
        public virtual Concept ConceptId1Navigation { get; set; }

        [ForeignKey(nameof(ConceptId2))]
        [InverseProperty(nameof(Concept.ConceptRelationshipConceptId2Navigations))]
        public virtual Concept ConceptId2Navigation { get; set; }

        [ForeignKey(nameof(RelationshipId))]
        [InverseProperty("ConceptRelationships")]
        public virtual Relationship Relationship { get; set; }
    }
}