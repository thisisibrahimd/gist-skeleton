using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("note_nlp")]
    [Index(nameof(NoteNlpConceptId), Name = "idx_note_nlp_concept_id")]
    [Index(nameof(NoteId), Name = "idx_note_nlp_note_id")]
    public class NoteNlp : BaseEntity, IAggregateRoot
    {
        [Key]
        [Column("note_nlp_id")]
        public long NoteNlpId { get; set; }

        [Column("note_id")]
        public int NoteId { get; set; }

        [Column("section_concept_id")]
        public int? SectionConceptId { get; set; }

        [Column("snippet")]
        [StringLength(250)]
        public string Snippet { get; set; }

        [Column("offset")]
        [StringLength(250)]
        public string Offset { get; set; }

        [Required]
        [Column("lexical_variant")]
        [StringLength(250)]
        public string LexicalVariant { get; set; }

        [Column("note_nlp_concept_id")]
        public int? NoteNlpConceptId { get; set; }

        [Column("note_nlp_source_concept_id")]
        public int? NoteNlpSourceConceptId { get; set; }

        [Column("nlp_system")]
        [StringLength(250)]
        public string NlpSystem { get; set; }

        [Column("nlp_date", TypeName = "date")]
        public DateTime NlpDate { get; set; }

        [Column("nlp_datetime")] public DateTime? NlpDatetime { get; set; }

        [Column("term_exists")]
        [StringLength(1)]
        public string TermExists { get; set; }

        [Column("term_temporal")]
        [StringLength(50)]
        public string TermTemporal { get; set; }

        [Column("term_modifiers")]
        [StringLength(2000)]
        public string TermModifiers { get; set; }

        [ForeignKey(nameof(NoteId))]
        [InverseProperty("NoteNlps")]
        public virtual Note Note { get; set; }

        [ForeignKey(nameof(NoteNlpConceptId))]
        [InverseProperty(nameof(Concept.NoteNlpNoteNlpConcepts))]
        public virtual Concept NoteNlpConcept { get; set; }

        [ForeignKey(nameof(SectionConceptId))]
        [InverseProperty(nameof(Concept.NoteNlpSectionConcepts))]
        public virtual Concept SectionConcept { get; set; }
    }
}