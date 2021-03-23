using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("concept_ancestor")]
    [Index(nameof(AncestorConceptId), Name = "idx_concept_ancestor_id_1")]
    [Index(nameof(DescendantConceptId), Name = "idx_concept_ancestor_id_2")]
    public class ConceptAncestor : BaseEntity, IAggregateRoot
    {
        [Key]
        [Column("ancestor_concept_id")]
        public int AncestorConceptId { get; set; }

        [Key]
        [Column("descendant_concept_id")]
        public int DescendantConceptId { get; set; }

        [Column("min_levels_of_separation")]
        public int MinLevelsOfSeparation { get; set; }

        [Column("max_levels_of_separation")]
        public int MaxLevelsOfSeparation { get; set; }

        [ForeignKey(nameof(AncestorConceptId))]
        [InverseProperty(nameof(Concept.ConceptAncestorAncestorConcepts))]
        public virtual Concept AncestorConcept { get; set; }

        [ForeignKey(nameof(DescendantConceptId))]
        [InverseProperty(nameof(Concept.ConceptAncestorDescendantConcepts))]
        public virtual Concept DescendantConcept { get; set; }
    }
}