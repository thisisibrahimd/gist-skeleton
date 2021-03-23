using Ardalis.Specification;
using Gist.ApplicationCore.Entities;

namespace Gist.ApplicationCore.Specification
{
    public sealed class CdmSourceCurrentSourceSpec : Specification<CdmSource>
    {
        public CdmSourceCurrentSourceSpec()
        {
            Query.OrderByDescending(cdmSource => cdmSource.CdmReleaseDate).Take(1);
        }
    }
}