using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Keyless]
    [Table("concept_synonym")]
    [Index(nameof(ConceptId), Name = "idx_concept_synonym_id")]
    public class ConceptSynonym : BaseEntity, IAggregateRoot
    {
        [Column("concept_id")]
        public int ConceptId { get; set; }

        [Required]
        [Column("concept_synonym_name")]
        [StringLength(1000)]
        public string ConceptSynonymName { get; set; }

        [Column("language_concept_id")]
        public int LanguageConceptId { get; set; }

        [ForeignKey(nameof(ConceptId))]
        public virtual Concept Concept { get; set; }
    }
}