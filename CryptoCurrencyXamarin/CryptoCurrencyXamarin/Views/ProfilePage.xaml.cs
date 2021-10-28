using CryptoCurrencyXamarin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CryptoCurrencyXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        ProfileVM VM;
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = VM = new ProfileVM();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            VM.GetownCurrency();
        }

    }
}