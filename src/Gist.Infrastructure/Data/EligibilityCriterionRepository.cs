using Gist.ApplicationCore.Entities;
using Gist.ApplicationCore.Interfaces;

namespace Gist.Infrastructure.Data
{
    public class EligibilityCriterionRepository : GistRepository<EligibilityCriterionEntity>, IEligibilityCriterionRepository
    {
        public EligibilityCriterionRepository(GistContext dbContext) : base(dbContext)
        {
        }
    }
}