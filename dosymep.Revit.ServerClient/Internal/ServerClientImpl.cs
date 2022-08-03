using System.IO;
using System.Threading;
using System.Threading.Tasks;

using dosymep.Revit.ServerClient.DataContracts;

namespace dosymep.Revit.ServerClient.Internal {
    /// <summary>
    /// The server client.
    /// </summary>
    internal class ServerClientImpl : IServerClient {
        private readonly IRevitHttpClient _httpClient;
        private readonly JsonSerialization _jsonSerialization;

        /// <summary>
        /// Creates instance server client.
        /// </summary>
        /// <param name="serverName">Server's name.</param>
        /// <param name="serverVersion">Server's version.</param>
        public ServerClientImpl(string serverName, string serverVersion) {
            ServerName = serverName;
            ServerVersion = serverVersion;

            _httpClient = new RevitHttpClient(serverName, serverVersion);
            _jsonSerialization = new JsonSerialization();
        }

        /// <inheritdoc />
        public string ServerName { get; }

        /// <inheritdoc />
        public string ServerVersion { get; }

        /// <inheritdoc />
        public Task<ServerProperties> GetServerPropertiesAsync(CancellationToken cancellationToken = default) {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Task<FolderContents> GetFolderContentsAsync(string folderPath,
            CancellationToken cancellationToken = default) {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Task<DirectoryData> GetDirectoryInformationAsync(string folderPath,
            CancellationToken cancellationToken = default) {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Task<ModelHistoryData> GetModelHistoryAsync(string modelPath,
            CancellationToken cancellationToken = default) {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Task<ModelInfoData> GetModelInformationAsync(string modelPath,
            CancellationToken cancellationToken = default) {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Task<Stream> GetModelThumbnailAsync(string modelPath, int width, int height,
            CancellationToken cancellationToken = default) {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Task<ProjectInfo> GetProjectInfoAsync(string modelPath, CancellationToken cancellationToken = default) {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Task LockAsync(string objectPath, CancellationToken cancellationToken = default) {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Task UnlockAsync(string objectPath, bool objectMustExist,
            CancellationToken cancellationToken = default) {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Task CancelLockAsync(string objectPath, CancellationToken cancellationToken = default) {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Task<LockedDescendentsData> GetDescendentsLocksAsync(string folderPath,
            CancellationToken cancellationToken = default) {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Task<UnlockDescendentsData> GetDescendentsUnlocksAsync(string folderPath,
            CancellationToken cancellationToken = default) {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Task CreateNewFolderAsync(string folderPath, CancellationToken cancellationToken = default) {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Task DeleteOrRenameAsync(string objectPath, string newObjectName,
            CancellationToken cancellationToken = default) {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Task CopyOrMoveAsync(string sourceObjectPath, string destinationObjectPath, PasteAction pasteAction,
            bool replaceExisting, CancellationToken cancellationToken = default) {
            throw new System.NotImplementedException();
        }
    }
}