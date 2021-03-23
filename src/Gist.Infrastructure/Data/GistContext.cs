using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Gist.ApplicationCore.Entities;

#nullable disable

namespace Gist.Infrastructure.Data
{
    public partial class GistContext : DbContext
    {
        public GistContext()
        {
        }

        public GistContext(DbContextOptions<GistContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EligibilityCriterionEntity> EligibilityCriterions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "C");

            modelBuilder.Entity<EligibilityCriterionEntity>(entity =>
            {
                entity.HasKey(e => new { e.NctId, e.ConceptId })
                    .HasName("pk_nct_concept");
            });
        }
    }
}
