using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Gist.ApplicationCore.Interfaces
{
    public interface ISvmService
    {
        /// <summary>
        ///     Perform C-Support Vector Machine Classification.
        /// </summary>
        /// <param name="features">A 2-Dimensional Array of doubles.</param>
        /// <param name="labels">An Array of doubles.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Predictions from C-Support Vector Machine Classification</returns>
        /// <exception cref="ArgumentNullException">
        ///     <c>features</c> and <c>labels</c> is
        ///     null or empty.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <c>features</c> and <c>labels</c> are not
        ///     of same length
        /// </exception>
        /// <exception cref="InvalidOperationException">Failed to perform svc calculations.</exception>
        /// <exception cref="HttpRequestException">
        ///     The request failed due to an underlying
        ///     issue such as network connectivity, DNS failure, server certificate
        ///     validation or timeout.
        /// </exception>
        /// <exception cref="TaskCanceledException">
        ///     .NET Core and .NET 5.0 and later only:
        ///     The request failed due to timeout.
        /// </exception>
        Task<double[]> SvcAsync(double[][] features, double[] labels,
            CancellationToken cancellationToken = default);
    }
}