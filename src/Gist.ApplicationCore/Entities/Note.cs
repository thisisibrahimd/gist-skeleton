using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("note")]
    [Index(nameof(NoteTypeConceptId), Name = "idx_note_concept_id")]
    [Index(nameof(PersonId), Name = "idx_note_person_id")]
    [Index(nameof(VisitOccurrenceId), Name = "idx_note_visit_id")]
    public class Note : BaseEntity, IAggregateRoot
    {
        public Note()
        {
            NoteNlps = new HashSet<NoteNlp>();
        }

        [Key]
        [Column("note_id")]
        public int NoteId { get; set; }

        [Column("person_id")]
        public int PersonId { get; set; }

        [Column("note_date", TypeName = "date")]
        public DateTime NoteDate { get; set; }

        [Column("note_datetime")]
        public DateTime? NoteDatetime { get; set; }

        [Column("note_type_concept_id")]
        public int NoteTypeConceptId { get; set; }

        [Column("note_class_concept_id")]
        public int NoteClassConceptId { get; set; }

        [Column("note_title")]
        [StringLength(250)]
        public string NoteTitle { get; set; }

        [Required]
        [Column("note_text")]
        public string NoteText { get; set; }

        [Column("encoding_concept_id")]
        public int EncodingConceptId { get; set; }

        [Column("language_concept_id")]
        public int LanguageConceptId { get; set; }

        [Column("provider_id")]
        public int? ProviderId { get; set; }

        [Column("visit_occurrence_id")]
        public int? VisitOccurrenceId { get; set; }

        [Column("note_source_value")]
        [StringLength(50)]
        public string NoteSourceValue { get; set; }

        [ForeignKey(nameof(EncodingConceptId))]
        [InverseProperty(nameof(Concept.NoteEncodingConcepts))]
        public virtual Concept EncodingConcept { get; set; }

        [ForeignKey(nameof(LanguageConceptId))]
        [InverseProperty(nameof(Concept.NoteLanguageConcepts))]
        public virtual Concept LanguageConcept { get; set; }

        [ForeignKey(nameof(NoteClassConceptId))]
        [InverseProperty(nameof(Concept.NoteNoteClassConcepts))]
        public virtual Concept NoteClassConcept { get; set; }

        [ForeignKey(nameof(NoteTypeConceptId))]
        [InverseProperty(nameof(Concept.NoteNoteTypeConcepts))]
        public virtual Concept NoteTypeConcept { get; set; }

        [ForeignKey(nameof(PersonId))]
        [InverseProperty("Notes")]
        public virtual Person Person { get; set; }

        [ForeignKey(nameof(ProviderId))]
        [InverseProperty("Notes")]
        public virtual Provider Provider { get; set; }

        [ForeignKey(nameof(VisitOccurrenceId))]
        [InverseProperty("Notes")]
        public virtual VisitOccurrence VisitOccurrence { get; set; }

        [InverseProperty(nameof(NoteNlp.Note))]
        public virtual ICollection<NoteNlp> NoteNlps { get; set; }
    }
}