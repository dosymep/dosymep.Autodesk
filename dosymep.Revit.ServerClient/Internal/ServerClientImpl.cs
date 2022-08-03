using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using dosymep.Revit.ServerClient.DataContracts;

using Newtonsoft.Json.Linq;

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
        public async Task<ServerProperties> GetServerPropertiesAsync(CancellationToken cancellationToken = default) {
            HttpResponseMessage response = await _httpClient.Get("serverProperties", cancellationToken);
            return _jsonSerialization.Deserialize<ServerProperties>(await response.Content.ReadAsStringAsync());
        }

        /// <inheritdoc />
        public async Task<FolderContents> GetFolderContentsAsync(string folderPath,
            CancellationToken cancellationToken = default) {
            if(string.IsNullOrEmpty(folderPath)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(folderPath));
            }

            folderPath = UpdateFolderPath(folderPath);
            HttpResponseMessage response = await _httpClient.Get($"{folderPath}/contents", cancellationToken);
            return _jsonSerialization.Deserialize<FolderContents>(await response.Content.ReadAsStringAsync());
        }

        /// <inheritdoc />
        public async Task<FolderInfoData> GetDirectoryInformationAsync(string folderPath,
            CancellationToken cancellationToken = default) {
            if(string.IsNullOrEmpty(folderPath)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(folderPath));
            }

            folderPath = UpdateFolderPath(folderPath);
            HttpResponseMessage response = await _httpClient.Get($"{folderPath}/DirectoryInfo", cancellationToken);
            return _jsonSerialization.Deserialize<FolderInfoData>(await response.Content.ReadAsStringAsync());
        }

        /// <inheritdoc />
        public async Task<ModelHistoryData> GetModelHistoryAsync(string modelPath,
            CancellationToken cancellationToken = default) {
            if(string.IsNullOrEmpty(modelPath)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(modelPath));
            }

            modelPath = UpdateFolderPath(modelPath);
            HttpResponseMessage response = await _httpClient.Get($"{modelPath}/history", cancellationToken);
            return _jsonSerialization.Deserialize<ModelHistoryData>(await response.Content.ReadAsStringAsync());
        }

        /// <inheritdoc />
        public async Task<ModelInfoData> GetModelInformationAsync(string modelPath,
            CancellationToken cancellationToken = default) {
            if(string.IsNullOrEmpty(modelPath)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(modelPath));
            }
            
            modelPath = UpdateFolderPath(modelPath);
            HttpResponseMessage response = await _httpClient.Get($"{modelPath}/modelInfo", cancellationToken);
            return _jsonSerialization.Deserialize<ModelInfoData>(await response.Content.ReadAsStringAsync());
        }

        /// <inheritdoc />
        public async Task<Stream> GetModelThumbnailAsync(string modelPath, int width, int height,
            CancellationToken cancellationToken = default) {
            if(string.IsNullOrEmpty(modelPath)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(modelPath));
            }

            if(width <= 0) {
                throw new ArgumentOutOfRangeException(nameof(width));
            }

            if(height <= 0) {
                throw new ArgumentOutOfRangeException(nameof(height));
            }

            modelPath = UpdateFolderPath(modelPath);
            HttpResponseMessage response = await _httpClient.Get($"{modelPath}/thumbnail?width={width}&height={height}", cancellationToken);
            return await response.Content.ReadAsStreamAsync();
        }

        /// <inheritdoc />
        public async Task<ProjectInfo> GetProjectInfoAsync(string modelPath, CancellationToken cancellationToken = default) {
            if(modelPath == null) {
                throw new ArgumentNullException(nameof(modelPath));
            }

            modelPath = UpdateFolderPath(modelPath);
            HttpResponseMessage response = await _httpClient.Get($"{modelPath}/projectInfo", cancellationToken);
            response.EnsureSuccessStatusCode();

            List<ParamInfoItem> items = _jsonSerialization.Deserialize<List<ParamInfoItem>>(await response.Content.ReadAsStringAsync());
            return new ProjectInfo() {Items = items};
        }

        /// <inheritdoc />
        public async Task LockAsync(string objectPath, CancellationToken cancellationToken = default) {
            if(objectPath == null) {
                throw new ArgumentNullException(nameof(objectPath));
            }

            objectPath = UpdateFolderPath(objectPath);
            await _httpClient.Put($"{objectPath}/lock", cancellationToken);
        }

        /// <inheritdoc />
        public async Task UnlockAsync(string objectPath, bool objectMustExist = false,
            CancellationToken cancellationToken = default) {
            if(string.IsNullOrEmpty(objectPath)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(objectPath));
            }

            objectPath = UpdateFolderPath(objectPath);
            await _httpClient.Delete($"{objectPath}/lock?objectMustExist={objectMustExist}", cancellationToken);
        }

        /// <inheritdoc />
        public async Task CancelLockAsync(string objectPath, CancellationToken cancellationToken = default) {
            if(string.IsNullOrEmpty(objectPath)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(objectPath));
            }

            objectPath = UpdateFolderPath(objectPath);
            await _httpClient.Delete($"{objectPath}/inProgressLock", cancellationToken);
        }

        /// <inheritdoc />
        public async Task<LockedDescendentsData> GetDescendentsLocksAsync(string folderPath,
            CancellationToken cancellationToken = default) {
            if(string.IsNullOrEmpty(folderPath)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(folderPath));
            }

            folderPath = UpdateFolderPath(folderPath);
            HttpResponseMessage response = await _httpClient.Get($"{folderPath}/descendent/locks", cancellationToken);
            return _jsonSerialization.Deserialize<LockedDescendentsData>(await response.Content.ReadAsStringAsync());
        }

        /// <inheritdoc />
        public async Task<UnlockDescendentsData> GetDescendentsUnlocksAsync(string folderPath,
            CancellationToken cancellationToken = default) {
            if(string.IsNullOrEmpty(folderPath)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(folderPath));
            }

            folderPath = UpdateFolderPath(folderPath);
            HttpResponseMessage response = await _httpClient.Get($"{folderPath}/descendent/locks", cancellationToken);
            return _jsonSerialization.Deserialize<UnlockDescendentsData>(await response.Content.ReadAsStringAsync());
        }

        /// <inheritdoc />
        public async Task CreateNewFolderAsync(string folderPath, CancellationToken cancellationToken = default) {
            if(string.IsNullOrEmpty(folderPath)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(folderPath));
            }

            folderPath = UpdateFolderPath(folderPath);
            await _httpClient.Put($"{folderPath}", cancellationToken);
        }

        /// <inheritdoc />
        public async Task DeleteOrRenameAsync(string objectPath, string newObjectName,
            CancellationToken cancellationToken = default) {
            if(string.IsNullOrEmpty(objectPath)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(objectPath));
            }

            if(string.IsNullOrEmpty(newObjectName)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(newObjectName));
            }

            objectPath = UpdateFolderPath(objectPath);
            await _httpClient.Delete($"{objectPath}?newObjectName={newObjectName}", cancellationToken);
        }

        /// <inheritdoc />
        public async Task CopyOrMoveAsync(string sourceObjectPath, string destinationObjectPath, PasteAction pasteAction,
            bool replaceExisting, CancellationToken cancellationToken = default) {
            if(string.IsNullOrEmpty(sourceObjectPath)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(sourceObjectPath));
            }

            if(string.IsNullOrEmpty(destinationObjectPath)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(destinationObjectPath));
            }

            sourceObjectPath = UpdateFolderPath(sourceObjectPath);
            destinationObjectPath = UpdateFolderPath(destinationObjectPath);
            await _httpClient.Post($"{sourceObjectPath}?destinationObjectPath={destinationObjectPath}&pasteAction={pasteAction}&replaceExisting={replaceExisting}", cancellationToken);
        }

        private static string UpdateFolderPath(string folderPath) {
            return folderPath.Replace('\\', '|').Replace('/', '|');
        }

        #region IDisposable

        /// <inheritdoc />
        public void Dispose() {
            _httpClient.Dispose();
        }

        #endregion
    }
}