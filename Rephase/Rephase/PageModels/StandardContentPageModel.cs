using Rephase.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Rephase.PageModels
{
    class StandardContentPageModel : PageModel, INotifyPropertyChanged
    {

        private ObservableCollection<MenuItems> children;
        public ObservableCollection<MenuItems> Children
        {
            get
            {
                return children;
            }
            set
            {
                children = value;
                NotifyPropertyChanged();
            }
        }
        
        private string icon;
        public string Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                NotifyPropertyChanged();
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                NotifyPropertyChanged();
            }
        }
    }
}
