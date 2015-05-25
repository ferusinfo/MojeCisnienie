using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MojeCisnienie.Resources;
using MojeCisnienie.ViewModels;
using Windows.Storage;
using Windows.Storage.Streams;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.Phone.Tasks;
using System.IO.IsolatedStorage;
using System.IO;
using System.Windows.Data;
using System.Globalization;
using System.Xml.Linq;

namespace MojeCisnienie
{
    public class IndexConverter : IValueConverter
    {
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            Cisnienie item = (Cisnienie)value;
            return App.ViewModel.HistoriaPomiarow.IndexOf(item).ToString();
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public partial class MainPage : PhoneApplicationPage
    {       
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // isolated storage global app settings
            IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

            // data file
            string filePath ="cisnienie.json";
            if (storage.FileExists(filePath))
            {
                clearHistory.Opacity = 1.00;
            }
            else
            {
                clearHistory.Opacity = 0;
            }

            string appVersion;
   
            //AppVersion
            using (var stream = new FileStream("WMAppManifest.xml", FileMode.Open, FileAccess.Read))
            {
                appVersion = XElement.Load(stream).Descendants("App").FirstOrDefault().Attribute("Version").Value;
                Debug.WriteLine(appVersion);
            }

            //settings.Add("needConversion", true);
            //settings.Save();
            

            if (!App.ViewModel.isDataLoaded)
            {
                // Load data here
                Debug.WriteLine("No data, loading it now");
                App.ViewModel.loadData();
            }

            DataContext = null;
            DataContext = App.ViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/CisnienieForm.xaml", UriKind.Relative));
        }

        private void selectorButtonTapped(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Button Tapped!");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult m = MessageBox.Show("Czy napewno chcesz usunąć historię pomiarow cisnienia?", "Potwierdzenie", MessageBoxButton.OKCancel);
            
            if (m == MessageBoxResult.OK)
            {
                IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication();
                string filePath = "cisnienie.json";
                if (storage.FileExists(filePath))
                    storage.DeleteFile(filePath);
                    clearHistory.Opacity = 0;
                
                App.ViewModel.loadData();
                DataContext = null;
                DataContext = App.ViewModel;
            }
        }

        private void rateApp_Click(object sender, RoutedEventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }

        private void cmdDeleteWpis_Click(object sender, RoutedEventArgs e)
        {
            var button = (sender as Button);
            var listID = button.Tag.ToString();
            var historyID = int.Parse(listID);
            Cisnienie cisnienie = App.ViewModel.HistoriaPomiarow.ElementAt(historyID);
            string msg = string.Format("Czy napewno chcesz usunąć pomiar z dnia: {0}?", cisnienie.Data.ToString("dd.MM.yyyy"));


             MessageBoxResult m = MessageBox.Show(msg, "Potwierdzenie", MessageBoxButton.OKCancel);

             if (m == MessageBoxResult.OK)
             {
                 App.ViewModel.removeItem(historyID);
                 App.ViewModel.loadData();
                 DataContext = null;
                 DataContext = App.ViewModel;
             }

        }

        

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}