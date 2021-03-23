using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("source_to_concept_map")]
    [Index(nameof(SourceCode), Name = "idx_source_to_concept_map_code")]
    [Index(nameof(SourceVocabularyId), Name = "idx_source_to_concept_map_id_1")]
    [Index(nameof(TargetVocabularyId), Name = "idx_source_to_concept_map_id_2")]
    [Index(nameof(TargetConceptId), Name = "idx_source_to_concept_map_id_3")]
    public class SourceToConceptMap : BaseEntity, IAggregateRoot
    {
        [Key]
        [Column("source_code")]
        [StringLength(50)]
        public string SourceCode { get; set; }

        [Column("source_concept_id")] public int SourceConceptId { get; set; }

        [Key]
        [Column("source_vocabulary_id")]
        [StringLength(20)]
        public string SourceVocabularyId { get; set; }

        [Column("source_code_description")]
        [StringLength(255)]
        public string SourceCodeDescription { get; set; }

        [Key]
        [Column("target_concept_id")]
        public int TargetConceptId { get; set; }

        [Required]
        [Column("target_vocabulary_id")]
        [StringLength(20)]
        public string TargetVocabularyId { get; set; }

        [Column("valid_start_date", TypeName = "date")]
        public DateTime ValidStartDate { get; set; }

        [Key]
        [Column("valid_end_date", TypeName = "date")]
        public DateTime ValidEndDate { get; set; }

        [Column("invalid_reason")]
        [StringLength(1)]
        public string InvalidReason { get; set; }

        [ForeignKey(nameof(SourceVocabularyId))]
        [InverseProperty(
            nameof(Vocabulary.SourceToConceptMapSourceVocabularies))]
        public virtual Vocabulary SourceVocabulary { get; set; }

        [ForeignKey(nameof(TargetConceptId))]
        [InverseProperty(nameof(Concept.SourceToConceptMaps))]
        public virtual Concept TargetConcept { get; set; }

        [ForeignKey(nameof(TargetVocabularyId))]
        [InverseProperty(
            nameof(Vocabulary.SourceToConceptMapTargetVocabularies))]
        public virtual Vocabulary TargetVocabulary { get; set; }
    }
}