using System;

using dosymep.Revit.ServerClient.Internal;

namespace dosymep.Revit.ServerClient {
    /// <summary>
    /// Class build server client.
    /// </summary>
    public class ServerClientBuilder {
        private string _serverName;
        private string _serverVersion;

        /// <summary>
        /// Sets server's name.
        /// </summary>
        /// <param name="serverName">Server's name.</param>
        /// <returns>Returns current builder.</returns>
        public ServerClientBuilder SetServerName(string serverName) {
            if(string.IsNullOrEmpty(serverName)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(serverName));
            }

            _serverName = serverName;
            return this;
        }

        /// <summary>
        /// Sets server's version.
        /// </summary>
        /// <param name="serverVersion">Server's version.</param>
        /// <returns>Returns current builder.</returns>
        public ServerClientBuilder SetServerVersion(string serverVersion) {
            if(string.IsNullOrEmpty(serverVersion)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(serverVersion));
            }

            _serverVersion = serverVersion;
            return this;
        }

        /// <summary>
        /// Creates connection with revit server.
        /// </summary>
        /// <returns>Returns connection with revit server .</returns>
        public IServerClient Build() {
            return new ServerClientImpl(_serverName, _serverVersion);
        }
    }
}