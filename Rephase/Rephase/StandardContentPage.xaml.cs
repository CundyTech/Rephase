using Rephase.Helpers;
using Rephase.Models;
using Rephase.PageModels;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rephase
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StandardContentPage : ContentPage
    {
        ITextToSpeechHelper TextToSpeechHelper;
        IMenuItemHelper MenuItemHelper;
        StandardContentPageModel StandardContentPageModel;
        const string MainMenuTitle = "Main Menu";

        public StandardContentPage(
            ITextToSpeechHelper textToSpeechHelper,
            IMenuItemHelper menuItemHelper,
            ObservableCollection<MenuItems> children,
            string title = null)
        {
            InitializeComponent();

            TextToSpeechHelper = textToSpeechHelper ?? throw new ArgumentNullException("TextToSpeechHelper");
            MenuItemHelper = menuItemHelper ?? throw new ArgumentNullException("MenuItemHelper");

            StandardContentPageModel = new StandardContentPageModel
            {
                Children = children,
                Icon = children[0].LocalImagePath,
                Title = title ?? MainMenuTitle
            };

            NavigationPage.SetTitleIcon(this, StandardContentPageModel.Icon);

            BindingContext = StandardContentPageModel;

            if (StandardContentPageModel.Title == MainMenuTitle)
            {
                NavigationPage.SetHasBackButton(this, false);
            }

            var labelTapGestureRecognizer = new TapGestureRecognizer();
            labelTapGestureRecognizer.Tapped += async (s, e) =>
            {
                await Button_ClickedAsync(s, e);
            };            
        }

        /// <summary>
        /// Overide back button. Disabled if on main menu.
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            if (Title == "Main Menu")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task TapGestureRecognizer_TappedAsync(object sender, EventArgs e)
        {
            try
            {
                if (sender is Image image)
                {
                    var title = ((MenuItems)image.BindingContext).Title;
                    await TextToSpeechHelper.ConvertTextToSpeechAsync(title);
                }
                if (sender is Button button)
                {
                    var title = ((MenuItems)button.BindingContext).Title;
                    await TextToSpeechHelper.ConvertTextToSpeechAsync(title);
                }
                else if (sender is ViewCell viewCell)
                {

                    var title = ((MenuItems)viewCell.BindingContext).Title;
                    var childList = MenuItemHelper.GetChild(title);
                    if (childList.Count != 0)
                    {
                        await Navigation.PushAsync(new StandardContentPage(TextToSpeechHelper, MenuItemHelper, childList, title));
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
        private async Task Button_ClickedAsync(object sender, EventArgs e)
        {
            try
            {
                IsEnabled = false;
                Button btn = (Button)sender;
                var title = ((ListItems)btn.BindingContext).Title;
                await TextToSpeechHelper.ConvertTextToSpeechAsync(title);
                IsEnabled = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
        private void Settings_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage());
        }             
    }
}

