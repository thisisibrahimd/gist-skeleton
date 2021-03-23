using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("relationship")]
    [Index(nameof(RelationshipId), Name = "idx_relationship_rel_id",
        IsUnique = true)]
    public class Relationship : BaseEntity, IAggregateRoot
    {
        public Relationship()
        {
            ConceptRelationships = new HashSet<ConceptRelationship>();
            InverseReverseRelationship = new HashSet<Relationship>();
        }

        [Key]
        [Column("relationship_id")]
        [StringLength(20)]
        public string RelationshipId { get; set; }

        [Required]
        [Column("relationship_name")]
        [StringLength(255)]
        public string RelationshipName { get; set; }

        [Required]
        [Column("is_hierarchical")]
        [StringLength(1)]
        public string IsHierarchical { get; set; }

        [Required]
        [Column("defines_ancestry")]
        [StringLength(1)]
        public string DefinesAncestry { get; set; }

        [Required]
        [Column("reverse_relationship_id")]
        [StringLength(20)]
        public string ReverseRelationshipId { get; set; }

        [Column("relationship_concept_id")]
        public int RelationshipConceptId { get; set; }

        [ForeignKey(nameof(RelationshipConceptId))]
        [InverseProperty(nameof(Concept.Relationships))]
        public virtual Concept RelationshipConcept { get; set; }

        [ForeignKey(nameof(ReverseRelationshipId))]
        [InverseProperty(nameof(InverseReverseRelationship))]
        public virtual Relationship ReverseRelationship { get; set; }

        [InverseProperty(nameof(ConceptRelationship.Relationship))]
        public virtual ICollection<ConceptRelationship> ConceptRelationships { get; set; }

        [InverseProperty(nameof(ReverseRelationship))]
        public virtual ICollection<Relationship> InverseReverseRelationship { get; set; }
    }
}