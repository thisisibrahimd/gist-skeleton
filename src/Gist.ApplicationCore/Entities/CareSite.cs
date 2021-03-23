using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("care_site")]
    public class CareSite : BaseEntity, IAggregateRoot
    {
        public CareSite()
        {
            People = new HashSet<Person>();
            Providers = new HashSet<Provider>();
            VisitOccurrences = new HashSet<VisitOccurrence>();
        }

        [Key]
        [Column("care_site_id")]
        public int CareSiteId { get; set; }

        [Column("care_site_name")]
        [StringLength(255)]
        public string CareSiteName { get; set; }

        [Column("place_of_service_concept_id")]
        public int? PlaceOfServiceConceptId { get; set; }

        [Column("location_id")]
        public int? LocationId { get; set; }

        [Column("care_site_source_value")]
        [StringLength(50)]
        public string CareSiteSourceValue { get; set; }

        [Column("place_of_service_source_value")]
        [StringLength(50)]
        public string PlaceOfServiceSourceValue { get; set; }

        [ForeignKey(nameof(LocationId))]
        [InverseProperty("CareSites")]
        public virtual Location Location { get; set; }

        [ForeignKey(nameof(PlaceOfServiceConceptId))]
        [InverseProperty(nameof(Concept.CareSites))]
        public virtual Concept PlaceOfServiceConcept { get; set; }

        [InverseProperty(nameof(Person.CareSite))]
        public virtual ICollection<Person> People { get; set; }

        [InverseProperty(nameof(Provider.CareSite))]
        public virtual ICollection<Provider> Providers { get; set; }

        [InverseProperty(nameof(VisitOccurrence.CareSite))]
        public virtual ICollection<VisitOccurrence> VisitOccurrences
        {
            get;
            set;
        }
    }
}