using System.Threading.Tasks;

namespace Rephase.Helpers
{
    public interface ILocalStorageHelper
    {
        /// <summary>
        /// Retrieve local storage path, create directory if it doesn't exist.
        /// </summary>
        /// <returns></returns>
        string GetContentJsonFromLocalStorage();

        /// <summary>
        /// Saves content json file to local storage folder json.
        /// </summary>
        /// <param name="json"></param>
        Task<string> RetrieveLocalStoragePathAsync();

        /// <summary>
        /// Retrieves content json file to local storage folder json.
        /// </summary>
        /// <param name="json"></param>
        Task SaveJsonToLocalStorageAsync(string json);
    }
}