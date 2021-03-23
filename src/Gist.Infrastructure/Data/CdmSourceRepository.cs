using Gist.ApplicationCore.Entities;
using Gist.ApplicationCore.Interfaces;

namespace Gist.Infrastructure.Data
{
    public class CdmSourceRepository : EhrRepository<CdmSource>, ICdmSourceReposiory
    {
        public CdmSourceRepository(EhrContext dbContext) : base(dbContext)
        {
        }
    }
}