using CryptoCurrencyXamarin.Helpers;
using CryptoCurrencyXamarin.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CryptoCurrencyXamarin.ViewModel
{
    public class FavoriteVM : BaseViewModel
    {
        public List<CryptoTableModel> Storedlist { get; set; }
        public ObservableCollection<CCInformationModel.Datum> FavoriteCryptolist { get; set; }

        public FavoriteVM()
        {
            FavoriteCryptolist = new ObservableCollection<CCInformationModel.Datum>();
            Con = new SQLiteConnection(App.DataBaseLocation);
            GetFavorite();
        }

        public void GetFavorite()
        {
            IsBusy = true;
            try
            {
                FavoriteCryptolist.Clear();
                Storedlist = new List<CryptoTableModel>();
                Con.CreateTable<CryptoTableModel>();
                Storedlist = Con.Table<CryptoTableModel>().ToList();

                if (Storedlist.Count > 0)
                {

                    if (CryptoStorelist.Count > 0)
                    {


                        foreach (var Item in CryptoStorelist)
                        {

                            var count = Storedlist.Where(I => I.symbol == Item.symbol).ToList();
                            if (count.Count > 0)
                            {
                                FavoriteCryptolist.Add(Item);

                            }
                        }

                    }
                }

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
    }
}
