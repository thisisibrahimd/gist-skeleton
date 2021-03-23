using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("specimen")]
    [Index(nameof(SpecimenConceptId), Name = "idx_specimen_concept_id")]
    [Index(nameof(PersonId), Name = "idx_specimen_person_id")]
    public class Speciman : BaseEntity, IAggregateRoot
    {
        [Key]
        [Column("specimen_id")]
        public int SpecimenId { get; set; }

        [Column("person_id")]
        public int PersonId { get; set; }

        [Column("specimen_concept_id")]
        public int SpecimenConceptId { get; set; }

        [Column("specimen_type_concept_id")]
        public int SpecimenTypeConceptId { get; set; }

        [Column("specimen_date", TypeName = "date")]
        public DateTime SpecimenDate { get; set; }

        [Column("specimen_datetime")]
        public DateTime? SpecimenDatetime { get; set; }

        [Column("quantity")]
        public decimal? Quantity { get; set; }

        [Column("unit_concept_id")]
        public int? UnitConceptId { get; set; }

        [Column("anatomic_site_concept_id")]
        public int? AnatomicSiteConceptId { get; set; }

        [Column("disease_status_concept_id")]
        public int? DiseaseStatusConceptId { get; set; }

        [Column("specimen_source_id")]
        [StringLength(50)]
        public string SpecimenSourceId { get; set; }

        [Column("specimen_source_value")]
        [StringLength(50)]
        public string SpecimenSourceValue { get; set; }

        [Column("unit_source_value")]
        [StringLength(50)]
        public string UnitSourceValue { get; set; }

        [Column("anatomic_site_source_value")]
        [StringLength(50)]
        public string AnatomicSiteSourceValue { get; set; }

        [Column("disease_status_source_value")]
        [StringLength(50)]
        public string DiseaseStatusSourceValue { get; set; }

        [ForeignKey(nameof(AnatomicSiteConceptId))]
        [InverseProperty(nameof(Concept.SpecimanAnatomicSiteConcepts))]
        public virtual Concept AnatomicSiteConcept { get; set; }

        [ForeignKey(nameof(DiseaseStatusConceptId))]
        [InverseProperty(nameof(Concept.SpecimanDiseaseStatusConcepts))]
        public virtual Concept DiseaseStatusConcept { get; set; }

        [ForeignKey(nameof(PersonId))]
        [InverseProperty("Specimen")]
        public virtual Person Person { get; set; }

        [ForeignKey(nameof(SpecimenConceptId))]
        [InverseProperty(nameof(Concept.SpecimanSpecimenConcepts))]
        public virtual Concept SpecimenConcept { get; set; }

        [ForeignKey(nameof(SpecimenTypeConceptId))]
        [InverseProperty(nameof(Concept.SpecimanSpecimenTypeConcepts))]
        public virtual Concept SpecimenTypeConcept { get; set; }

        [ForeignKey(nameof(UnitConceptId))]
        [InverseProperty(nameof(Concept.SpecimanUnitConcepts))]
        public virtual Concept UnitConcept { get; set; }
    }
}