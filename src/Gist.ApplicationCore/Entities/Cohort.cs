using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("cohort")]
    [Index(nameof(CohortDefinitionId), Name = "idx_cohort_c_definition_id")]
    [Index(nameof(SubjectId), Name = "idx_cohort_subject_id")]
    public class Cohort : BaseEntity, IAggregateRoot
    {
        [Key]
        [Column("cohort_definition_id")]
        public int CohortDefinitionId { get; set; }

        [Key]
        [Column("subject_id")]
        public int SubjectId { get; set; }

        [Key]
        [Column("cohort_start_date", TypeName = "date")]
        public DateTime CohortStartDate { get; set; }

        [Key]
        [Column("cohort_end_date", TypeName = "date")]
        public DateTime CohortEndDate { get; set; }

        [ForeignKey(nameof(CohortDefinitionId))]
        [InverseProperty("Cohorts")]
        public virtual CohortDefinition CohortDefinition { get; set; }
    }
}