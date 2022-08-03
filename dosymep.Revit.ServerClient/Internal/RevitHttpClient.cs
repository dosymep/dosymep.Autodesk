﻿using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace dosymep.Revit.ServerClient.Internal {
    /// <summary>
    /// The revit http client.
    /// </summary>
    public class RevitHttpClient : IRevitHttpClient {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Creates instance with revit server.
        /// </summary>
        /// <param name="serverName">Server's name.</param>
        /// <param name="serverVersion">Server's version.</param>
        public RevitHttpClient(string serverName, string serverVersion) {
            if(string.IsNullOrEmpty(serverName)) {
                throw new ArgumentException($"'{nameof(serverName)}' cannot be null or empty.", nameof(serverName));
            }

            if(string.IsNullOrEmpty(serverVersion)) {
                throw new ArgumentException($"'{nameof(serverVersion)}' cannot be null or empty.",
                    nameof(serverVersion));
            }

            _baseUrl = $"http://{serverName}/RevitServerAdminRESTService{serverVersion}/AdminRESTService.svc";

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Name", Environment.UserName);
            _httpClient.DefaultRequestHeaders.Add("User-Machine-Name", Environment.MachineName);
        }

        /// <inheritdoc/>
        public Task<HttpResponseMessage> Get(string requestUri, CancellationToken cancellationToken = default) {
            if(string.IsNullOrEmpty(requestUri)) {
                throw new ArgumentException($"'{nameof(requestUri)}' cannot be null or empty.", nameof(requestUri));
            }

            return _httpClient.SendAsync(CreateHttpRequestMessage(HttpMethod.Get, requestUri), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<HttpResponseMessage> Put(string requestUri, CancellationToken cancellationToken = default) {
            if(string.IsNullOrEmpty(requestUri)) {
                throw new ArgumentException($"'{nameof(requestUri)}' cannot be null or empty.", nameof(requestUri));
            }

            return _httpClient.SendAsync(CreateHttpRequestMessage(HttpMethod.Put, requestUri), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<HttpResponseMessage> Post(string requestUri, CancellationToken cancellationToken = default) {
            if(string.IsNullOrEmpty(requestUri)) {
                throw new ArgumentException($"'{nameof(requestUri)}' cannot be null or empty.", nameof(requestUri));
            }

            return _httpClient.SendAsync(CreateHttpRequestMessage(HttpMethod.Post, requestUri), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<HttpResponseMessage> Delete(string requestUri, CancellationToken cancellationToken = default) {
            if(string.IsNullOrEmpty(requestUri)) {
                throw new ArgumentException($"'{nameof(requestUri)}' cannot be null or empty.", nameof(requestUri));
            }

            return _httpClient.SendAsync(CreateHttpRequestMessage(HttpMethod.Delete, requestUri), cancellationToken);
        }

        private HttpRequestMessage CreateHttpRequestMessage(HttpMethod httpMethod, string requestUri) {
            var requestMessage = new HttpRequestMessage(httpMethod, _baseUrl + "/" + requestUri);
            requestMessage.Headers.Add("Operation-GUID", Guid.NewGuid().ToString());

            return requestMessage;
        }

        #region IDisposable

        /// <inheritdoc />
        public void Dispose() {
            _httpClient.Dispose();
        }

        #endregion

    }
}