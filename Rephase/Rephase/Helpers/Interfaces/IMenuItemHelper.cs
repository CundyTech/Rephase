using System.Collections.ObjectModel;
using Rephase.Models;

namespace Rephase.Helpers
{
    public interface IMenuItemHelper
    {
        /// <summary>
        /// Converts content json to a list of menuitems.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        ObservableCollection<MenuItems> ConvertToObservableList(string json);

        /// <summary>
        /// Retrieve the child of a menu item given a key.
        /// </summary>
        /// <param name="parentKey"></param>
        /// <returns></returns>
        ObservableCollection<MenuItems> GetChild(string parentKey);
    }
}