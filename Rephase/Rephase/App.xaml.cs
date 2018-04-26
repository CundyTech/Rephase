using Rephase.Helpers;

using Xamarin.Forms;

namespace Rephase
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ConnectivityManager connectivityManager = new ConnectivityManager();
            SerialisationHelper serialisationHelper = new SerialisationHelper();
            LocalStorageHelper localStorageHelper = new LocalStorageHelper();
            MenuItemHelper menuItemHelper = new MenuItemHelper(serialisationHelper, localStorageHelper);
            AzureStorageHelper azureStorageHelper = new AzureStorageHelper();
            ImageHelper imageHelper = new ImageHelper(azureStorageHelper);
            TextToSpeechHelper textToSpeechHelper = new TextToSpeechHelper();
            
            MainPage = new NavigationPage(
                new SplashScreenPage(
                    connectivityManager,
                    menuItemHelper,
                    azureStorageHelper,
                    imageHelper,
                    textToSpeechHelper,
                    localStorageHelper));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
