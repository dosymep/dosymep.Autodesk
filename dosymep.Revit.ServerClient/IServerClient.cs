using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using dosymep.Revit.ServerClient.DataContracts;

namespace dosymep.Revit.ServerClient {
    /// <summary>
    /// Provides connection to revit server.
    /// </summary>
    public interface IServerClient : IDisposable {
        /// <summary>
        /// The server name.
        /// </summary>
        string ServerName { get; }
        
        /// <summary>
        /// The server version.
        /// </summary>
        string ServerVersion { get; }
        
        /// <summary>
        /// URL: GET /serverProperties
        /// Queries the server’s properties.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns the server’s properties.</returns>
        Task<ServerProperties> GetServerPropertiesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// URL: GET /{folderPath}/contents
        /// Queries the contents of a folder.
        /// </summary>
        /// <param name="folderPath">The path of the specified folder.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns the contents of a folder.</returns>
        Task<FolderContents> GetFolderContentsAsync(string folderPath, CancellationToken cancellationToken = default);

        /// <summary>
        /// URL: GET /{folderPath}/DirectoryInfo
        /// Queries the folder directory information.
        /// </summary>
        /// <param name="folderPath">The path of the specified folder.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns the folder directory information.</returns>
        Task<DirectoryData> GetDirectoryInformationAsync(string folderPath, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// URL: GET /{modelPath}/history
        /// Queries the submission history of a model.
        /// </summary>
        /// <param name="modelPath">The path of the specified model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns the submission history of a model.</returns>
        Task<ModelHistoryData> GetModelHistoryAsync(string modelPath, CancellationToken cancellationToken = default);

        /// <summary>
        /// URL: GET /{modelPath}/modelInfo
        /// Queries the file information of a model.
        /// </summary>
        /// <param name="modelPath">The path of the specified model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns the file information of a model.</returns>
        Task<ModelInfoData> GetModelInformationAsync(string modelPath, CancellationToken cancellationToken = default);

        /// <summary>
        /// URL: GET /{modelPath}/thumbnail?width={width}&amp;height={height}
        /// Gets the thumbnail of a model.
        /// </summary>
        /// <param name="modelPath">The path of the specified model.</param>
        /// <param name="width">Width of expected thumbnail.</param>
        /// <param name="height">Height of expected thumbnail.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns the thumbnail of a model.</returns>
        Task<Stream> GetModelThumbnailAsync(string modelPath, int width, int height, CancellationToken cancellationToken = default);

        /// <summary>
        /// URL: GET /{modelPath}/projectInfo
        /// Queries the project information of a model.
        /// </summary>
        /// <param name="modelPath">The path of the specified model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns the project information of a model.</returns>
        Task<ProjectInfo> GetProjectInfoAsync(string modelPath, CancellationToken cancellationToken = default);

        /// <summary>
        /// URL: PUT /{objectPath}/lock
        /// Lock the server, a folder or a model.
        /// </summary>
        /// <param name="objectPath">The path of the server, the specified folder or the specified model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task LockAsync(string objectPath, CancellationToken cancellationToken = default);

        /// <summary>
        /// URL: DELETE /{objectPath}/lock?objectMustExist={objectMustExist}
        /// Unlocks the server, a folder, or a model.
        /// </summary>
        /// <param name="objectPath">The path of the server, the specified folder, or the specified model.</param>
        /// <param name="objectMustExist">Whether the folder or model must exist.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task UnlockAsync(string objectPath, bool objectMustExist = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// URL: DELETE /{objectPath}/inProgressLock
        /// Cancel the in-progress locking operation on the server, a folder, or a model.
        /// </summary>
        /// <param name="objectPath">The path of the server, the specified folder, or the specified model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task CancelLockAsync(string objectPath, CancellationToken cancellationToken = default);

        /// <summary>
        /// URL: GET /{folderPath}/descendent/locks
        /// Gets the lock information of the descendents of a folder.
        /// </summary>
        /// <param name="folderPath">The path of the specified folder.</param>
        /// <returns>Returns the lock information of the descendents of a folder.</returns>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task<LockedDescendentsData> GetDescendentsLocksAsync(string folderPath, CancellationToken cancellationToken = default);

        /// <summary>
        /// URL: DELETE /{folderPath}/descendent/locks
        /// Unlocks all locks of the descendents of a folder.
        /// </summary>
        /// <param name="folderPath">The path of the specified folder.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns all locks of the descendents of a folder.</returns>
        Task<UnlockDescendentsData> GetDescendentsUnlocksAsync(string folderPath, CancellationToken cancellationToken = default);

        /// <summary>
        /// URL: PUT /{folderPath}
        /// Creates a new folder.
        /// </summary>
        /// <param name="folderPath">The path of the new folder to be created.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task CreateNewFolderAsync(string folderPath, CancellationToken cancellationToken = default);

        /// <summary>
        /// URL: DELETE /{objectPath}?newObjectName={newObjectName}
        /// Rename or delete a folder or model.
        /// </summary>
        /// <param name="objectPath">The path of the specified folder or model.</param>
        /// <param name="newObjectName">New name for the folder or model. Empty value means the object will be deleted.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task DeleteOrRenameAsync(string objectPath, string newObjectName, CancellationToken cancellationToken = default);

        /// <summary>
        /// URL: POST /{sourceObjectPath}?destinationObjectPath={destinationObjectPath}&amp;pasteAction={pasteAction}&amp;replaceExisting={replaceExisting}
        /// Copies or moves a folder or a model to another folder or model.
        /// </summary>
        /// <param name="sourceObjectPath">The path of the source object.</param>
        /// <param name="destinationObjectPath">The path of the destination object.</param>
        /// <param name="pasteAction">The action type.</param>
        /// <param name="replaceExisting">True if to replace destination object if it already exists, otherwise false.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task CopyOrMoveAsync(
            string sourceObjectPath,
            string destinationObjectPath,
            PasteAction pasteAction,
            bool replaceExisting,
            CancellationToken cancellationToken = default);
    }
}