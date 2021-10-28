using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Environment = System.Environment;
using System.IO;

namespace CryptoCurrencyXamarin.Droid
{

    [Activity(Label = "Crypto Currency Xamarin", Icon = "@drawable/cryptocurrencylogo", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            string DataBaseName = "FavoriteCurrency_DB.sqlite";
            string FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string FullPath = Path.Combine(FolderPath, DataBaseName);
            LoadApplication(new App(FullPath));

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}