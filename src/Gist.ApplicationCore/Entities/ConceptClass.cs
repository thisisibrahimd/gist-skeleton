using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("concept_class")]
    [Index(nameof(ConceptClassId), Name = "idx_concept_class_class_id",
        IsUnique = true)]
    public class ConceptClass : BaseEntity, IAggregateRoot
    {
        public ConceptClass()
        {
            Concepts = new HashSet<Concept>();
        }

        [Key]
        [Column("concept_class_id")]
        [StringLength(20)]
        public string ConceptClassId { get; set; }

        [Required]
        [Column("concept_class_name")]
        [StringLength(255)]
        public string ConceptClassName { get; set; }

        [Column("concept_class_concept_id")]
        public int ConceptClassConceptId { get; set; }

        [ForeignKey(nameof(ConceptClassConceptId))]
        [InverseProperty(nameof(Concept.ConceptClasses))]
        public virtual Concept ConceptClassConcept { get; set; }

        [InverseProperty(nameof(Concept.ConceptClass))]
        public virtual ICollection<Concept> Concepts { get; set; }
    }
}