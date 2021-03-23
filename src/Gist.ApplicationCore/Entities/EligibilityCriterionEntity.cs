using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("eligibility_criterion")]
    public class EligibilityCriterionEntity : BaseEntity, IAggregateRoot
    {
        [Key]
        [Column("nct_id")]
        [StringLength(255)]
        public string NctId { get; set; }
        
        [Column("concept_name")]
        [StringLength(255)]
        public string ConceptName { get; set; }
        
        [Key]
        [Column("concept_id")]
        public int ConceptId { get; set; }
        
        [Required]
        [Column("domain_name")]
        [StringLength(255)]
        public string DomainName { get; set; }
        
        [Column("cat_elig")]
        public int CatElig { get; set; }
        
        [Column("lab_elig_min")]
        public double? LabEligMin { get; set; }
        
        [Column("lab_elig_max")]
        public double? LabEligMax { get; set; }
    }
}
