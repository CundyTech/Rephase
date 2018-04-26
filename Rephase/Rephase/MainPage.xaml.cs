using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Rephase
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            var pageModel = new MainPageModel();
            pageModel.WelcomeMessage = "Welcome to Rephase!";

            BindingContext = pageModel;                      
        }
	}
}
