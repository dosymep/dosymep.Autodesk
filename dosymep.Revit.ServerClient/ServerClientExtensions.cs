using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using dosymep.Revit.ServerClient.DataContracts;

namespace dosymep.Revit.ServerClient {
    /// <summary>
    /// Class contains server client extensions.
    /// </summary>
    public static class ServerClientExtensions {
        /// <summary>
        /// Returns relative model path.
        /// </summary>
        /// <param name="folderContents">Parent folder contents.</param>
        /// <param name="objectData">Object data.</param>
        /// <returns>Returns relative model path.</returns>
        public static string GetRelativeModelPath(this FolderContents folderContents, ObjectData objectData) {
            return Path.Combine(folderContents.Path, objectData.Name);
        }

        /// <summary>
        /// Returns root folder contents.
        /// </summary>
        /// <param name="serverClient">Server client connection.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns root folder contents.</returns>
        public static Task<FolderContents> GetRootFolderContentsAsync(this IServerClient serverClient,
            CancellationToken cancellationToken = default) {
            return serverClient.GetFolderContentsAsync("|", cancellationToken);
        }

        /// <summary>
        /// Returns all revit server contents.
        /// </summary>
        /// <param name="serverClient">Server client connection.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns all revit contents.</returns>
        public static Task<List<FolderContents>> GetRecursiveFolderContentsAsync(this IServerClient serverClient,
            CancellationToken cancellationToken = default) {
            return serverClient.GetRecursiveFolderContentsAsync("|", cancellationToken);
        }

        /// <summary>
        /// Returns all revit server contents.
        /// </summary>
        /// <param name="serverClient">Server client connection.</param>
        /// <param name="folderPath">The path of the specified folder.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns all revit server contents.</returns>
        public static async Task<List<FolderContents>> GetRecursiveFolderContentsAsync(this IServerClient serverClient,
            string folderPath, CancellationToken cancellationToken = default) {
            try {
                FolderContents contents = await serverClient.GetFolderContentsAsync(folderPath, cancellationToken);

                IEnumerable<Task<List<FolderContents>>> tasks = contents.Folders
                    .Select(item =>
                        serverClient.GetRecursiveFolderContentsAsync(contents.GetRelativeModelPath(item),
                            cancellationToken));

                List<FolderContents>[] result = await Task.WhenAll(tasks);
                return result.SelectMany(item => item).Prepend(contents).ToList();
            } catch {
                // RS is not deterministic because
                // it can return folder paths
                // that throw a 404 not found exception
                return new List<FolderContents>();
            }
        }

        /// <summary>
        /// Returns visible model path for RS.
        /// </summary>
        /// <param name="serverClient">Server client connection.</param>
        /// <param name="folderContents">Parent folder contents.</param>
        /// <param name="objectData">Object data.</param>
        /// <returns>Returns visible model path for RS.</returns>
        public static string GetVisibleModelPath(this IServerClient serverClient,
            FolderContents folderContents, ObjectData objectData) {
            return Path.Combine($"RSN://{serverClient.ServerName}", folderContents.GetRelativeModelPath(objectData));
        }
    }
}