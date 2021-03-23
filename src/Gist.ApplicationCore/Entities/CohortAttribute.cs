using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("cohort_attribute")]
    [Index(nameof(CohortDefinitionId), Name = "idx_ca_definition_id")]
    [Index(nameof(SubjectId), Name = "idx_ca_subject_id")]
    public class CohortAttribute : BaseEntity, IAggregateRoot
    {
        [Key]
        [Column("cohort_definition_id")]
        public int CohortDefinitionId { get; set; }

        [Key]
        [Column("cohort_start_date", TypeName = "date")]
        public DateTime CohortStartDate { get; set; }

        [Key]
        [Column("cohort_end_date", TypeName = "date")]
        public DateTime CohortEndDate { get; set; }

        [Key]
        [Column("subject_id")]
        public int SubjectId { get; set; }

        [Key]
        [Column("attribute_definition_id")]
        public int AttributeDefinitionId { get; set; }

        [Column("value_as_number")]
        public decimal? ValueAsNumber { get; set; }

        [Column("value_as_concept_id")]
        public int? ValueAsConceptId { get; set; }

        [ForeignKey(nameof(AttributeDefinitionId))]
        [InverseProperty("CohortAttributes")]
        public virtual AttributeDefinition AttributeDefinition { get; set; }

        [ForeignKey(nameof(CohortDefinitionId))]
        [InverseProperty("CohortAttributes")]
        public virtual CohortDefinition CohortDefinition { get; set; }

        [ForeignKey(nameof(ValueAsConceptId))]
        [InverseProperty(nameof(Concept.CohortAttributes))]
        public virtual Concept ValueAsConcept { get; set; }
    }
}