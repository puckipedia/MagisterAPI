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

namespace PoehoeWIP
{
    public partial class MainPage : PhoneApplicationPage
    {
        School School;
        User User;
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
            this.Loaded += MainPage_Loaded;
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            System.Globalization.CultureInfo ci =
                System.Threading.Thread.CurrentThread.CurrentCulture;
            DayOfWeek fdow = ci.DateTimeFormat.FirstDayOfWeek;
            DayOfWeek today = DateTime.Now.DayOfWeek;
            DateTime sow = DateTime.Now.AddDays(-(today - fdow)).Date;
            await DoStuff(sow);
        }

        public async Task DoStuff(DateTime Week)
        {
            School = School ?? new School("chrlyceumdelft");

            await School.SchoolVersion();

            User = User ?? await School.Login("118556", "$PASS");

            DateTime Start = Week;
            DateTime End = Week.AddDays(4);
            string Stamnummer = "118556";

            var Requ = await AgendaRequest.Create(Start, End, User, Stamnummer).Send(User);

            AgendaMapper Mapper = AgendaMapper.GetData(Requ, User);
            this.DataContext = Mapper;
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
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