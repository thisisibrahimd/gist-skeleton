using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("domain")]
    [Index(nameof(DomainId), Name = "idx_domain_domain_id", IsUnique = true)]
    public class Domain : BaseEntity, IAggregateRoot
    {
        public Domain()
        {
            Concepts = new HashSet<Concept>();
        }

        [Key]
        [Column("domain_id")]
        [StringLength(20)]
        public string DomainId { get; set; }

        [Required]
        [Column("domain_name")]
        [StringLength(255)]
        public string DomainName { get; set; }

        [Column("domain_concept_id")]
        public int DomainConceptId { get; set; }

        [ForeignKey(nameof(DomainConceptId))]
        [InverseProperty(nameof(Concept.Domains))]
        public virtual Concept DomainConcept { get; set; }

        [InverseProperty(nameof(Concept.Domain))]
        public virtual ICollection<Concept> Concepts { get; set; }
    }
}