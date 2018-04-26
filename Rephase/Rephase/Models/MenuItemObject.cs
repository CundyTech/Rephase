using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rephase.Models
{
    /// <summary>
    /// This is the object that is passed between the create section to the menu section.
    /// </summary>
    public class MenuItemObject
    {
        public string Title { get; set; }
        public string Json { get; set; }
    }
}
