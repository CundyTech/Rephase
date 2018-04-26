using PCLStorage;
using System.IO;
using System.Threading.Tasks;

namespace Rephase.Helpers
{
    class LocalStorageHelper : ILocalStorageHelper
    {        
        /// <summary>
        /// Retrieve local storage path, create directory if it doesn't exist.
        /// </summary>
        /// <returns></returns>
        public async Task<string> RetrieveLocalStoragePathAsync()
        {
            // Access the file system for the current platform.
            IFileSystem fileSystem = FileSystem.Current;

            // Get the root directory of the file system.
            IFolder rootFolder = fileSystem.LocalStorage;

            // Create folder, if it doesn't already exist.
            IFolder localStoragePath = await rootFolder.CreateFolderAsync("Content", CreationCollisionOption.OpenIfExists);

            return localStoragePath.Path;
        }

        /// <summary>
        /// Saves content json file to local storage folder json.
        /// </summary>
        /// <param name="json"></param>
        public async Task SaveJsonToLocalStorageAsync(string json)
        {
            // Access the file system for the current platform.
            IFileSystem fileSystem = FileSystem.Current;

            // Get the root directory of the file system.
            IFolder rootFolder = fileSystem.LocalStorage;

            // Create a file, if one doesn't already exist.
            IFile jsonFile = await rootFolder.CreateFileAsync("content.txt", CreationCollisionOption.ReplaceExisting);

            File.WriteAllText(jsonFile.Path, json);
        }

        /// <summary>
        /// Retrieves content json file to local storage folder json.
        /// </summary>
        /// <param name="json"></param>
        public string GetContentJsonFromLocalStorage()
        {
            // Access the file system for the current platform.
            IFileSystem fileSystem = FileSystem.Current;

            // Get the root directory of the file system.
            IFolder rootFolder = fileSystem.LocalStorage;

            var jsonFilePath = Path.Combine(rootFolder.Path, "content.txt");

            if (!File.Exists(jsonFilePath))
            {
                return null;
            }

            return File.ReadAllText(jsonFilePath);
        }
    }
}
