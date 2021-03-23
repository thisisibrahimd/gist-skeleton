using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("vocabulary")]
    [Index(nameof(VocabularyId), Name = "idx_vocabulary_vocabulary_id",
        IsUnique = true)]
    public class Vocabulary : BaseEntity, IAggregateRoot
    {
        public Vocabulary()
        {
            Concepts = new HashSet<Concept>();
            SourceToConceptMapSourceVocabularies =
                new HashSet<SourceToConceptMap>();
            SourceToConceptMapTargetVocabularies =
                new HashSet<SourceToConceptMap>();
        }

        [Key]
        [Column("vocabulary_id")]
        [StringLength(20)]
        public string VocabularyId { get; set; }

        [Required]
        [Column("vocabulary_name")]
        [StringLength(255)]
        public string VocabularyName { get; set; }

        [Column("vocabulary_reference")]
        [StringLength(255)]
        public string VocabularyReference { get; set; }

        [Column("vocabulary_version")]
        [StringLength(255)]
        public string VocabularyVersion { get; set; }

        [Column("vocabulary_concept_id")]
        public int VocabularyConceptId { get; set; }

        [ForeignKey(nameof(VocabularyConceptId))]
        [InverseProperty(nameof(Concept.Vocabularies))]
        public virtual Concept VocabularyConcept { get; set; }

        [InverseProperty(nameof(Concept.Vocabulary))]
        public virtual ICollection<Concept> Concepts { get; set; }

        [InverseProperty(nameof(SourceToConceptMap.SourceVocabulary))]
        public virtual ICollection<SourceToConceptMap> SourceToConceptMapSourceVocabularies { get; set; }

        [InverseProperty(nameof(SourceToConceptMap.TargetVocabulary))]
        public virtual ICollection<SourceToConceptMap> SourceToConceptMapTargetVocabularies { get; set; }
    }
}