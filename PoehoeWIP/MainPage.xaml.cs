using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PoehoeWIP.Resources;
using Poehoe;
using Welp;
using System.Threading.Tasks;
using System.Globalization;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Threading;

namespace PoehoeWIP
{
    public partial class MainPage : PhoneApplicationPage
    {
        School School;
        User User;

        public class Data
        {
            public AgendaMapper Mapper
            {
                get;
                set;
            }

            public string Title
            {
                get;
                set;
            }
        }
        DateTime StartOfWeek;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            Poehoe.Crypto.ZipCrypto = new PoehoeZipWin32.ZipCrypto();
            Poehoe.Crypto.AesKey = "087EC4624E964AE27DBDFE03279A2EE4";
            Poehoe.Crypto.ZipKey = "yawUBRu+reduka5UPha2#=cRUc@ThekawEvuju&?g$dru9ped=a@REQ!7h_?anut";
            Poehoe.Crypto.AesCrypto = new AesCrypto();


            //DoStuff(DateTime.Today).Wait();
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

            DayOfWeek fdow = DayOfWeek.Monday;
            DayOfWeek today = DateTime.Now.DayOfWeek;
            DateTime sow = DateTime.Now.AddDays(-(today - fdow)).Date;
            StartOfWeek = sow;
        }

        public async Task DoStuff(DateTime Week)
        {
            var WeekNum = DateTimeFormatInfo.CurrentInfo.Calendar.GetWeekOfYear(Week, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            var PI = new ProgressIndicator()
            {
                IsIndeterminate = false,
                Text = "Loading week " + WeekNum,
                Value = 0,
                IsVisible = true
            };
            try
            {

                SystemTray.SetProgressIndicator(this, PI);

                var A = IsolatedStorageSettings.ApplicationSettings;
                var Username = A["Username"] as string;
                var Password = A["Password"] as string;
                var SchoolText = A["School"] as string;

                if (School != null && School.SchoolNaam != SchoolText)
                {
                    School = null;
                    User = null;
                }

                if (User != null && User.Username != Username)
                    User = null;

                School = School ?? new School(SchoolText);

                await School.SchoolVersion();

                PI.Value = 0.25;

                User = User ?? await School.Login(Username, Password);

                var Data = await User.GetLeerlingData();

                PI.Value = 0.5;

                DateTime Start = Week;
                DateTime End = Week.AddDays(4);
                string Stamnummer = Data.stamnr + "";

                var Requ = await AgendaRequest.Create(Start, End, User, Stamnummer).Send(User);

                PI.Value = 0.75;

                AgendaMapper Mapper = AgendaMapper.GetData(Requ, User);
                if(Week == StartOfWeek)
                    this.DataContext = new Data() { Title = "Week " + WeekNum, Mapper = Mapper };

                PI.Value = 1;

                await Task.Delay(2000);
                PI.IsVisible = false;
            }
            catch (Exception e)
            {
                PI.Text = "Error updating!";
                Thread.Sleep(2000);
                PI.IsVisible = false;
            }
        }

        internal static AgendaItem AgItem;

        protected void GetInfo(object sender, RoutedEventArgs e)
        {
            MenuItem I = (MenuItem)sender;
            var A = (AgendaItem)I.DataContext;
            AgItem = A;
            NavigationService.Navigate(new Uri("/Info.xaml", UriKind.Relative));
        }

        // Load data for the ViewModel Items
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await DoStuff(StartOfWeek);
        }

        private async void VorigeWeek(object sender, EventArgs e)
        {
            StartOfWeek = StartOfWeek.AddDays(-7);
            await DoStuff(StartOfWeek);
        }

        private async void VolgendeWeek(object sender, EventArgs e)
        {
            StartOfWeek = StartOfWeek.AddDays(7);
            await DoStuff(StartOfWeek);
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
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