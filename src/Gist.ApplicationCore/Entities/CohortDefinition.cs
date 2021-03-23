using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("cohort_definition")]
    [Index(nameof(CohortDefinitionId), Name = "idx_cohort_definition_id")]
    public class CohortDefinition : BaseEntity, IAggregateRoot
    {
        public CohortDefinition()
        {
            CohortAttributes = new HashSet<CohortAttribute>();
            Cohorts = new HashSet<Cohort>();
        }

        [Key]
        [Column("cohort_definition_id")]
        public int CohortDefinitionId { get; set; }

        [Required]
        [Column("cohort_definition_name")]
        [StringLength(255)]
        public string CohortDefinitionName { get; set; }

        [Column("cohort_definition_description")]
        public string CohortDefinitionDescription { get; set; }

        [Column("definition_type_concept_id")]
        public int DefinitionTypeConceptId { get; set; }

        [Column("cohort_definition_syntax")]
        public string CohortDefinitionSyntax { get; set; }

        [Column("subject_concept_id")]
        public int SubjectConceptId { get; set; }

        [Column("cohort_initiation_date", TypeName = "date")]
        public DateTime? CohortInitiationDate { get; set; }

        [ForeignKey(nameof(DefinitionTypeConceptId))]
        [InverseProperty(nameof(Concept.CohortDefinitions))]
        public virtual Concept DefinitionTypeConcept { get; set; }

        [InverseProperty(nameof(CohortAttribute.CohortDefinition))]
        public virtual ICollection<CohortAttribute> CohortAttributes { get; set; }

        [InverseProperty(nameof(Cohort.CohortDefinition))]
        public virtual ICollection<Cohort> Cohorts { get; set; }
    }
}