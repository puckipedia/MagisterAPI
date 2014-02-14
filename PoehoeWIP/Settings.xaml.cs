using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;

namespace PoehoeWIP
{
    public partial class Settings : PhoneApplicationPage
    {
        public Settings()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var A = IsolatedStorageSettings.ApplicationSettings;
            Username.Text = A.Contains("Username") ? A["Username"] as string : "";
            Password.Password = A.Contains("Password") ? A["Password"] as string : "";
            School.Text = A.Contains("School") ? A["School"] as string : "";
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            var A = IsolatedStorageSettings.ApplicationSettings;
            A["Username"] = Username.Text;
            A["Password"] = Password.Password;
            A["School"] = School.Text;
            A.Save();
            base.OnNavigatingFrom(e);
        }
    }
}