using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("payer_plan_period")]
    [Index(nameof(PersonId), Name = "idx_period_person_id")]
    public class PayerPlanPeriod : BaseEntity, IAggregateRoot
    {
        public PayerPlanPeriod()
        {
            Costs = new HashSet<Cost>();
        }

        [Key]
        [Column("payer_plan_period_id")]
        public int PayerPlanPeriodId { get; set; }

        [Column("person_id")]
        public int PersonId { get; set; }

        [Column("payer_plan_period_start_date", TypeName = "date")]
        public DateTime PayerPlanPeriodStartDate { get; set; }

        [Column("payer_plan_period_end_date", TypeName = "date")]
        public DateTime PayerPlanPeriodEndDate { get; set; }

        [Column("payer_source_value")]
        [StringLength(50)]
        public string PayerSourceValue { get; set; }

        [Column("plan_source_value")]
        [StringLength(50)]
        public string PlanSourceValue { get; set; }

        [Column("family_source_value")]
        [StringLength(50)]
        public string FamilySourceValue { get; set; }

        [ForeignKey(nameof(PersonId))]
        [InverseProperty("PayerPlanPeriods")]
        public virtual Person Person { get; set; }

        [InverseProperty(nameof(Cost.PayerPlanPeriod))]
        public virtual ICollection<Cost> Costs { get; set; }
    }
}