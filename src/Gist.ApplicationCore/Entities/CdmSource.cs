using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Keyless]
    [Table("cdm_source")]
    public class CdmSource : BaseEntity, IAggregateRoot
    {
        [Required]
        [Column("cdm_source_name")]
        [StringLength(255)]
        public string CdmSourceName { get; set; }

        [Column("cdm_source_abbreviation")]
        [StringLength(25)]
        public string CdmSourceAbbreviation { get; set; }

        [Column("cdm_holder")]
        [StringLength(255)]
        public string CdmHolder { get; set; }

        [Column("source_description")]
        public string SourceDescription { get; set; }

        [Column("source_documentation_reference")]
        [StringLength(255)]
        public string SourceDocumentationReference { get; set; }

        [Column("cdm_etl_reference")]
        [StringLength(255)]
        public string CdmEtlReference { get; set; }

        [Column("source_release_date", TypeName = "date")]
        public DateTime? SourceReleaseDate { get; set; }

        [Column("cdm_release_date", TypeName = "date")]
        public DateTime? CdmReleaseDate { get; set; }

        [Column("cdm_version")]
        [StringLength(10)]
        public string CdmVersion { get; set; }

        [Column("vocabulary_version")]
        [StringLength(20)]
        public string VocabularyVersion { get; set; }
    }
}