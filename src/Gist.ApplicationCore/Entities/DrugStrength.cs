using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("drug_strength")]
    [Index(nameof(DrugConceptId), Name = "idx_drug_strength_id_1")]
    [Index(nameof(IngredientConceptId), Name = "idx_drug_strength_id_2")]
    public class DrugStrength : BaseEntity, IAggregateRoot
    {
        [Key]
        [Column("drug_concept_id")]
        public int DrugConceptId { get; set; }

        [Key]
        [Column("ingredient_concept_id")]
        public int IngredientConceptId { get; set; }

        [Column("amount_value")]
        public decimal? AmountValue { get; set; }

        [Column("amount_unit_concept_id")]
        public int? AmountUnitConceptId { get; set; }

        [Column("numerator_value")]
        public decimal? NumeratorValue { get; set; }

        [Column("numerator_unit_concept_id")]
        public int? NumeratorUnitConceptId { get; set; }

        [Column("denominator_value")]
        public decimal? DenominatorValue { get; set; }

        [Column("denominator_unit_concept_id")]
        public int? DenominatorUnitConceptId { get; set; }

        [Column("box_size")]
        public int? BoxSize { get; set; }

        [Column("valid_start_date", TypeName = "date")]
        public DateTime ValidStartDate { get; set; }

        [Column("valid_end_date", TypeName = "date")]
        public DateTime ValidEndDate { get; set; }

        [Column("invalid_reason")]
        [StringLength(1)]
        public string InvalidReason { get; set; }

        [ForeignKey(nameof(AmountUnitConceptId))]
        [InverseProperty(nameof(Concept.DrugStrengthAmountUnitConcepts))]
        public virtual Concept AmountUnitConcept { get; set; }

        [ForeignKey(nameof(DenominatorUnitConceptId))]
        [InverseProperty(nameof(Concept.DrugStrengthDenominatorUnitConcepts))]
        public virtual Concept DenominatorUnitConcept { get; set; }

        [ForeignKey(nameof(DrugConceptId))]
        [InverseProperty(nameof(Concept.DrugStrengthDrugConcepts))]
        public virtual Concept DrugConcept { get; set; }

        [ForeignKey(nameof(IngredientConceptId))]
        [InverseProperty(nameof(Concept.DrugStrengthIngredientConcepts))]
        public virtual Concept IngredientConcept { get; set; }

        [ForeignKey(nameof(NumeratorUnitConceptId))]
        [InverseProperty(nameof(Concept.DrugStrengthNumeratorUnitConcepts))]
        public virtual Concept NumeratorUnitConcept { get; set; }
    }
}