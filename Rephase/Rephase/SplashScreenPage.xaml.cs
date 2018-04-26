using Rephase.Helpers;
using Rephase.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rephase
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashScreenPage : ContentPage
    {
        IConnectivityManager ConnectivityManager;
        IMenuItemHelper MenuItemHelper;
        IAzureStorageHelper AzureStorageHelper;
        IImageHelper ImageHelper;
        ITextToSpeechHelper TextToSpeechHelper;
        ILocalStorageHelper LocalStorageHelper;
        private SplashScreenPageModel pageModel;

        public SplashScreenPage(
            IConnectivityManager connectivityManager,
            IMenuItemHelper menuItemHelper,
            IAzureStorageHelper azureStorageHelper,
            IImageHelper imageHelper,
            ITextToSpeechHelper textToSpeechHelper,
            ILocalStorageHelper localStorageHelper)
        {
            ConnectivityManager = connectivityManager ?? throw new NullReferenceException("ConnectivityManager");
            MenuItemHelper = menuItemHelper ?? throw new NullReferenceException("MenuItemHelper");
            AzureStorageHelper = azureStorageHelper ?? throw new NullReferenceException("AzureStorageHelper");
            ImageHelper = imageHelper ?? throw new NullReferenceException("ImageHelper");
            TextToSpeechHelper = textToSpeechHelper ?? throw new NullReferenceException("TextToSpeechHelper");
            LocalStorageHelper = localStorageHelper ?? throw new NullReferenceException("LocalStorageHelper");

            InitializeComponent();

            pageModel = new SplashScreenPageModel();
            pageModel.WelcomeMessage = "Welcome to Rephase!";
            BindingContext = pageModel;

            //Hide Navigation bar.
            NavigationPage.SetHasNavigationBar(this, false);

            ApplicationStartAsync();
        }

        /// <summary>
        /// Collect content json, any assets not already 
        /// downloaded and then navigate to main content page.
        /// </summary>
        /// <returns></returns>
        private async Task ApplicationStartAsync()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            if (ConnectivityManager.IsInternetAccessable())
            {
                await CollectAssetsAsync();
            }

            await Navigation.PushAsync(new StandardContentPage(
                TextToSpeechHelper,
                MenuItemHelper,
                MenuItemHelper.ConvertToObservableList(
                    LocalStorageHelper.GetContentJsonFromLocalStorage())
                    ));
        }

        /// <summary>
        /// Collect and download all assets needed.
        /// </summary>
        /// <returns></returns>
        private async Task CollectAssetsAsync()
        {
            try
            {
                List<ImageDownload> imageDownloads = new List<ImageDownload>();

                pageModel.ProgressText = "Collecting information...";

                await LocalStorageHelper.SaveJsonToLocalStorageAsync(await AzureStorageHelper.DownloadContentJsonAsync());

                imageDownloads = CollectImagesToBeDownloaded(
                    MenuItemHelper.ConvertToObservableList(
                        LocalStorageHelper.GetContentJsonFromLocalStorage()), imageDownloads);

                int downloadCount = imageDownloads.Count;

                // Get all images in local file store
                string[] fileEntries = Directory.GetFiles(await LocalStorageHelper.RetrieveLocalStoragePathAsync());

                // work out if we have already downloaded each file.
                for (int i = 0; i < imageDownloads.Count; i++)
                {
                    if (!fileEntries.ToList().Any(x => x.ToLowerInvariant() == imageDownloads[i].LocalImagePath.ToLowerInvariant()))
                    {
                        pageModel.ProgressText = string.Format("Downloading {0} of {1}", i + 1, imageDownloads.Count);
                        UpdateProgressBar(i, downloadCount);
                        await DownloadImagesAsync(imageDownloads[i]);
                    }
                }

                pageModel.ProgressText = "Finished, loading main page...";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Work out progress and update
        /// </summary>
        /// <param name="value"></param>
        /// <param name="total"></param>
        public void UpdateProgressBar(dynamic value, int total)
        {
            float progress = (float)value / (float)total;
            pageModel.Progress = progress;
        }

        private Task<ImageDownload> DownloadImagesAsync(ImageDownload download)
        {
            return Task.Run(() =>
            {
                return ImageHelper.SaveImageToLocalStorageAsync(download);
            });
        }

        private List<ImageDownload> CollectImagesToBeDownloaded(ObservableCollection<MenuItems> oc, List<ImageDownload> urls)
        {

            for (int i = 0; i < oc.Count; i++)
            {
                var imageDownload = new ImageDownload
                {
                    ImageUrl = oc[i].ImageUrl,
                    Title = oc[i].Title,
                    LocalImagePath = oc[i].LocalImagePath
                };

                urls.Add(imageDownload);

                if (oc[i].Child.Count > 0)
                {
                    CollectImagesToBeDownloaded(oc[i].Child, urls);
                }
            }

            return urls;
        }

    }
}