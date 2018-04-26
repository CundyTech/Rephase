using System.Collections.Generic;
using Newtonsoft.Json;
using Rephase.Models;

namespace Rephase.Helpers
{
    class SerialisationHelper : ISerialisationHelper
    {
        /// <summary>
        /// Serialise menu items to json
        /// </summary>
        /// <param name="menuItems"></param>
        /// <returns></returns>
        public string SerialiseMenuItems(List<MenuItems> menuItems)
        {
            return JsonConvert.SerializeObject(menuItems);
        }

        /// <summary>
        /// Deserialise json to T object. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="menuItems"></param>
        /// <returns></returns>
        public T DeserialiseJson<T>(string menuItems)
        {
            return JsonConvert.DeserializeObject<T>(menuItems);
        }
    }
}