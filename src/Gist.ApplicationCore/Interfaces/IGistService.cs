using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gist.ApplicationCore.Entities;
using Gist.ApplicationCore.Models;

namespace Gist.ApplicationCore.Interfaces
{
    public interface IGistService
    {
        /// <summary>
        ///     Calculate the S + M GIST scores with provided criteria and people
        /// </summary>
        /// <param name="criteria">A list of Criterion.</param>
        /// <param name="people">A list of People and their EMRs</param>
        /// <returns>GIST Result which contains the S GIST scores and the M GIST Score</returns>
        Task<GistScore> GetGistScore(EligibilityCriteria criteria,
            Person[] people);
    }
}