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
    public partial class favorite : ContentPage
    {
        FavoriteVM VM;

        public favorite()
        {
            InitializeComponent();
            BindingContext = VM = new FavoriteVM();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            VM.GetFavorite();

        }
    }
}