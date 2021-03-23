using System.Collections.Generic;
using System.Threading.Tasks;
using Gist.ApplicationCore.Entities;

namespace Gist.ApplicationCore.Interfaces
{
    public interface IPersonRepository : IAsyncRepository<Person>
    {
        Task<IReadOnlyList<Person>> GetPeopleEmrByConceptIds(int[] conceptIds);
    }
}