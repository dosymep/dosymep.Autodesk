using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace dosymep.Revit.ServerClient.Internal {
    /// <summary>
    /// Interface HTTP connection with server.
    /// </summary>
    internal interface IRevitHttpClient {
        /// <summary>
        /// Generate GET request to the server.
        /// </summary>
        /// <param name="requestUri">Request url.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns server response.</returns>
        Task<HttpResponseMessage> Get(string requestUri, CancellationToken cancellationToken = default);

        /// <summary>
        /// Generate PUT request to the server.
        /// </summary>
        /// <param name="requestUri">Request url.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns server response.</returns>
        Task<HttpResponseMessage> Put(string requestUri, CancellationToken cancellationToken = default);

        /// <summary>
        /// Generate POST request to the server.
        /// </summary>
        /// <param name="requestUri">Request url.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns server response.</returns>
        Task<HttpResponseMessage> Post(string requestUri, CancellationToken cancellationToken = default);

        /// <summary>
        /// Generate DELETE request to the server.
        /// </summary>
        /// <param name="requestUri">Request url.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns server response.</returns>
        Task<HttpResponseMessage> Delete(string requestUri, CancellationToken cancellationToken = default);
    }
}