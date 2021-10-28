using CryptoCurrencyXamarin.Helpers;
using CryptoCurrencyXamarin.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace CryptoCurrencyXamarin.ViewModel
{
    public class ProfileVM : BaseViewModel
    {
        public ObservableCollection<Profile> OwnCryptolist { get; set; }


        public Command<Profile> Add { get; set; }
        public Command<Profile> decrease { get; set; }




        public ProfileVM()
        {
            Con = new SQLiteConnection(App.DataBaseLocation);
            OwnCryptolist = new ObservableCollection<Profile>();
            Add = new Command<Profile>(addcurrency);
            decrease = new Command<Profile>(reduce);
            GeFund();
            GetownCurrency();
        }

        private void addcurrency(Profile obj)
        {
            increase(obj);
            GetownCurrency();
        }

        private void reduce(Profile obj)
        {
            var result = RestApi.GetAsync<CCInformationModel.Root>("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");
            if (result.data.Count > 0)
            {
                foreach (var i in result.data)
                {
                    if (i.symbol == obj.symbol)
                    {


                        Con.Delete(obj);
                        UpdateFund(Fund + i.quote.USD.price);

                    }
                }

            }
            GetownCurrency();
        }
        public void GetownCurrency()
        {
            OwnCryptolist.Clear();
            Con.CreateTable<Profile>();
            var Storedlist = Con.Table<Profile>().ToList();
            if (Storedlist.Count > 0)
            {
                foreach (var Item in Storedlist)
                {
                    OwnCryptolist.Add(Item);
                }
            }
            GeFund();

        }





    }
}
