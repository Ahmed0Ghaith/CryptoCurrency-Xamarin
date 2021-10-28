using CryptoCurrencyXamarin.Helpers;
using CryptoCurrencyXamarin.Models;
using CryptoCurrencyXamarin.Views;
using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace CryptoCurrencyXamarin.ViewModel
{
    class MainPageVM : BaseViewModel
    {


        public ObservableCollection<CCInformationModel.Datum> Cryptolist { get; set; }
        public ObservableCollection<CCInformationModel.Datum> FavoriteCryptolist { get; set; }
        public List<CCInformationModel.Datum> CashCryptolist { get; set; }
        public List<CryptoTableModel> Storedlist { get; set; }


        public Command refresh { get; set; }
        public Command NavToProfiel { get; set; }

        public Command<CCInformationModel.Datum> AddFav { get; set; }

        public Command<CCInformationModel.Datum> Add { get; set; }


        private string _search;
        public string Search
        {
            get => _search;
            set
            {

                SetProperty(ref _search, value);
                search();
            }
        }




        public MainPageVM()
        {
            refresh = new Command(GetCurrency);
            AddFav = new Command<CCInformationModel.Datum>(AddFavorite);
            Cryptolist = new ObservableCollection<CCInformationModel.Datum>();
            FavoriteCryptolist = new ObservableCollection<CCInformationModel.Datum>();
            NavToProfiel = new Command(NavProfile);
            Add = new Command<CCInformationModel.Datum>(AddCurrency);
            CryptoStorelist = new List<CCInformationModel.Datum>();
            Con = new SQLiteConnection(App.DataBaseLocation);
            Con.CreateTable<Profile>();
            GetCurrency();
            GeFund();


        }

        private void AddCurrency(CCInformationModel.Datum obj)
        {
            Profile NewCurrency = new Profile
            {

                name = obj.name,
                price = obj.quote.USD.price,
                symbol = obj.symbol
            };
            increase(NewCurrency);

        }



        private void NavProfile(object obj)
        {
            App.Current.MainPage.Navigation.PushAsync(new ProfilePage());
        }

        private void AddFavorite(CCInformationModel.Datum obj)
        {
            obj.Fav = !obj.Fav;
            CashCryptolist = new List<CCInformationModel.Datum>();
            CashCryptolist.AddRange(Cryptolist);
            Cryptolist.Clear();


            foreach (var Item in CashCryptolist)
            {
                Cryptolist.Add(Item);
            }
            CryptoTableModel FAVItem = new CryptoTableModel
            {
                id = obj.id,
                name = obj.name,
                symbol = obj.symbol,
                Fav = obj.Fav
            };
            if (!obj.Fav)
            {
                Con.Delete(FAVItem);
            }
            else
            {
                Con.Insert(FAVItem);

            }
            //GetCurrency();
        }

        public void GetCurrency()
        {
            IsBusy = true;
            try
            {
                Storedlist = new List<CryptoTableModel>();
                Con.CreateTable<CryptoTableModel>();
                Storedlist = Con.Table<CryptoTableModel>().ToList();


                var result = RestApi.GetAsync<CCInformationModel.Root>("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");
                CryptoStorelist.Clear();

                foreach (var Item in result.data)
                {
                    Item.Fav = false;
                    if (Storedlist.Count > 0)
                    {
                        var count = Storedlist.Where(I => I.symbol == Item.symbol).ToList();
                        if (count.Count > 0)
                        {
                            Item.Fav = true;

                        }
                    }
                    CryptoStorelist.Add(Item);
                }
                search();
            }
            catch (System.Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error", ex.Message, "Cancel");
            }
            finally
            {
                IsBusy = false;

            }
        }
        private void search()
        {

            Cryptolist.Clear();
            if (string.IsNullOrEmpty(Search))
            {
                foreach (var Item in CryptoStorelist)
                {
                    Cryptolist.Add(Item);
                }

            }
            else
            {
                var SList = CryptoStorelist.Where(I => I.name.Contains(Search));
                foreach (var Item in SList)
                {
                    Cryptolist.Add(Item);
                }
            }


        }






    }
}
