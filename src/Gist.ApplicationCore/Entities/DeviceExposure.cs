using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("device_exposure")]
    [Index(nameof(DeviceConceptId), Name = "idx_device_concept_id")]
    [Index(nameof(PersonId), Name = "idx_device_person_id")]
    [Index(nameof(VisitOccurrenceId), Name = "idx_device_visit_id")]
    public class DeviceExposure : BaseEntity, IAggregateRoot
    {
        [Key]
        [Column("device_exposure_id")]
        public int DeviceExposureId { get; set; }

        [Column("person_id")]
        public int PersonId { get; set; }

        [Column("device_concept_id")]
        public int DeviceConceptId { get; set; }

        [Column("device_exposure_start_date", TypeName = "date")]
        public DateTime DeviceExposureStartDate { get; set; }

        [Column("device_exposure_start_datetime")]
        public DateTime DeviceExposureStartDatetime { get; set; }

        [Column("device_exposure_end_date", TypeName = "date")]
        public DateTime? DeviceExposureEndDate { get; set; }

        [Column("device_exposure_end_datetime")]
        public DateTime? DeviceExposureEndDatetime { get; set; }

        [Column("device_type_concept_id")]
        public int DeviceTypeConceptId { get; set; }

        [Column("unique_device_id")]
        [StringLength(50)]
        public string UniqueDeviceId { get; set; }

        [Column("quantity")] public int? Quantity { get; set; }

        [Column("provider_id")] public int? ProviderId { get; set; }

        [Column("visit_occurrence_id")]
        public int? VisitOccurrenceId { get; set; }

        [Column("device_source_value")]
        [StringLength(100)]
        public string DeviceSourceValue { get; set; }

        [Column("device_source_concept_id")]
        public int? DeviceSourceConceptId { get; set; }

        [ForeignKey(nameof(DeviceConceptId))]
        [InverseProperty(nameof(Concept.DeviceExposureDeviceConcepts))]
        public virtual Concept DeviceConcept { get; set; }

        [ForeignKey(nameof(DeviceSourceConceptId))]
        [InverseProperty(nameof(Concept.DeviceExposureDeviceSourceConcepts))]
        public virtual Concept DeviceSourceConcept { get; set; }

        [ForeignKey(nameof(DeviceTypeConceptId))]
        [InverseProperty(nameof(Concept.DeviceExposureDeviceTypeConcepts))]
        public virtual Concept DeviceTypeConcept { get; set; }

        [ForeignKey(nameof(PersonId))]
        [InverseProperty("DeviceExposures")]
        public virtual Person Person { get; set; }

        [ForeignKey(nameof(ProviderId))]
        [InverseProperty("DeviceExposures")]
        public virtual Provider Provider { get; set; }

        [ForeignKey(nameof(VisitOccurrenceId))]
        [InverseProperty("DeviceExposures")]
        public virtual VisitOccurrence VisitOccurrence { get; set; }
    }
}