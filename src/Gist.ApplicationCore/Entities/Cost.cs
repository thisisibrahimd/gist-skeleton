using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("cost")]
    public class Cost : BaseEntity, IAggregateRoot
    {
        [Key]
        [Column("cost_id")]
        public int CostId { get; set; }

        [Column("cost_event_id")]
        public int CostEventId { get; set; }

        [Required]
        [Column("cost_domain_id")]
        [StringLength(20)]
        public string CostDomainId { get; set; }

        [Column("cost_type_concept_id")]
        public int CostTypeConceptId { get; set; }

        [Column("currency_concept_id")]
        public int? CurrencyConceptId { get; set; }

        [Column("total_charge")]
        public decimal? TotalCharge { get; set; }

        [Column("total_cost")]
        public decimal? TotalCost { get; set; }

        [Column("total_paid")]
        public decimal? TotalPaid { get; set; }

        [Column("paid_by_payer")]
        public decimal? PaidByPayer { get; set; }

        [Column("paid_by_patient")]
        public decimal? PaidByPatient { get; set; }

        [Column("paid_patient_copay")]
        public decimal? PaidPatientCopay { get; set; }

        [Column("paid_patient_coinsurance")]
        public decimal? PaidPatientCoinsurance { get; set; }

        [Column("paid_patient_deductible")]
        public decimal? PaidPatientDeductible { get; set; }

        [Column("paid_by_primary")]
        public decimal? PaidByPrimary { get; set; }

        [Column("paid_ingredient_cost")]
        public decimal? PaidIngredientCost { get; set; }

        [Column("paid_dispensing_fee")]
        public decimal? PaidDispensingFee { get; set; }

        [Column("payer_plan_period_id")]
        public int? PayerPlanPeriodId { get; set; }

        [Column("amount_allowed")]
        public decimal? AmountAllowed { get; set; }

        [Column("revenue_code_concept_id")]
        public int? RevenueCodeConceptId { get; set; }

        [Column("reveue_code_source_value")]
        [StringLength(50)]
        public string ReveueCodeSourceValue { get; set; }

        [Column("drg_concept_id")]
        public int? DrgConceptId { get; set; }

        [Column("drg_source_value")]
        [StringLength(3)]
        public string DrgSourceValue { get; set; }

        [ForeignKey(nameof(CurrencyConceptId))]
        [InverseProperty(nameof(Concept.CostCurrencyConcepts))]
        public virtual Concept CurrencyConcept { get; set; }

        [ForeignKey(nameof(DrgConceptId))]
        [InverseProperty(nameof(Concept.CostDrgConcepts))]
        public virtual Concept DrgConcept { get; set; }

        [ForeignKey(nameof(PayerPlanPeriodId))]
        [InverseProperty("Costs")]
        public virtual PayerPlanPeriod PayerPlanPeriod { get; set; }
    }
}