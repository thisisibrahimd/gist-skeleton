using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("attribute_definition")]
    [Index(nameof(AttributeDefinitionId), Name = "idx_attribute_definition_id")]
    public class AttributeDefinition : BaseEntity, IAggregateRoot
    {
        public AttributeDefinition()
        {
            CohortAttributes = new HashSet<CohortAttribute>();
        }

        [Key]
        [Column("attribute_definition_id")]
        public int AttributeDefinitionId { get; set; }

        [Required]
        [Column("attribute_name")]
        [StringLength(255)]
        public string AttributeName { get; set; }

        [Column("attribute_description")]
        public string AttributeDescription { get; set; }

        [Column("attribute_type_concept_id")]
        public int AttributeTypeConceptId { get; set; }

        [Column("attribute_syntax")]
        public string AttributeSyntax { get; set; }

        [InverseProperty(nameof(CohortAttribute.AttributeDefinition))]
        public virtual ICollection<CohortAttribute> CohortAttributes
        {
            get;
            set;
        }
    }
}