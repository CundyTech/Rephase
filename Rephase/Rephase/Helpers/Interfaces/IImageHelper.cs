using System.Threading.Tasks;
using Rephase.Models;

namespace Rephase.Helpers
{
    public interface IImageHelper
    {
        /// <summary>
        /// Saves given image object to local storage.
        /// </summary>
        /// <param name="download"></param>
        /// <returns></returns>
        Task<ImageDownload> SaveImageToLocalStorageAsync(ImageDownload download);
    }
}