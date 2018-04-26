using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Rephase.PageModels
{
    //PageModel Base class
    class PageModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
