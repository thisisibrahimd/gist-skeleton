using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gist.ApplicationCore.Interfaces;

#nullable disable

namespace Gist.ApplicationCore.Entities
{
    [Table("location")]
    public class Location : BaseEntity, IAggregateRoot
    {
        public Location()
        {
            CareSites = new HashSet<CareSite>();
            People = new HashSet<Person>();
        }

        [Key]
        [Column("location_id")]
        public int LocationId { get; set; }

        [Column("address_1")]
        [StringLength(50)]
        public string Address1 { get; set; }

        [Column("address_2")]
        [StringLength(50)]
        public string Address2 { get; set; }

        [Column("city")]
        [StringLength(50)]
        public string City { get; set; }

        [Column("state")]
        [StringLength(2)]
        public string State { get; set; }

        [Column("zip")]
        [StringLength(9)]
        public string Zip { get; set; }

        [Column("county")]
        [StringLength(20)]
        public string County { get; set; }

        [Column("location_source_value")]
        [StringLength(50)]
        public string LocationSourceValue { get; set; }

        [InverseProperty(nameof(CareSite.Location))]
        public virtual ICollection<CareSite> CareSites { get; set; }

        [InverseProperty(nameof(Person.Location))]
        public virtual ICollection<Person> People { get; set; }
    }
}