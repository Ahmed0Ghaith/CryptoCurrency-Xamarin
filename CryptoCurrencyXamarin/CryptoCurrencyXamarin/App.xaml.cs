using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CryptoCurrencyXamarin
{
    public partial class App : Application
    {
        public static string DataBaseLocation = string.Empty;

        public App()
        {
            InitializeComponent();

            MainPage = new Views.MainPage();
        }
        public App(String _DataBaseLocation)
        {
            InitializeComponent();
            DataBaseLocation = _DataBaseLocation;
            MainPage = new Views.MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
