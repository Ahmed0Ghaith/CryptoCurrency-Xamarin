using CryptoCurrencyXamarin.Helpers;
using CryptoCurrencyXamarin.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;

namespace CryptoCurrencyXamarin.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {


        public static List<CCInformationModel.Datum> CryptoStorelist { get; set; }

        private double _fund;
        public double Fund
        {
            get => _fund;
            set => SetProperty(ref _fund, value);
        }

        private bool _isbusy = false;
        public bool IsBusy
        {
            get => _isbusy;
            set => SetProperty(ref _isbusy, value);
        }




        private bool _isErrorState = false;
        public bool IsErrorState
        {
            get => _isErrorState;
            set => SetProperty(ref _isErrorState, value);
        }

        private string _errorMessage = string.Empty;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        private string _errorImage = string.Empty;
        public string ErrorImage
        {
            get => _errorImage;
            set => SetProperty(ref _errorImage, value);
        }

        public SQLiteConnection Con;


        public void increase(Profile obj)
        {
            var result = RestApi.GetAsync<CCInformationModel.Root>("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");
            if (result.data.Count > 0)
            {
                foreach (var i in result.data)
                {
                    if (i.symbol == obj.symbol)
                    {
                        if (i.quote.USD.price <= Fund)
                        {
                            Profile Addnew = new Profile
                            {
                                name = i.name,
                                price = i.quote.USD.price,
                                symbol = i.symbol
                            };
                            Con.Insert(Addnew);
                            UpdateFund(Fund - i.quote.USD.price);

                        }
                        else
                        {
                            App.Current.MainPage.DisplayAlert("Alert", "You don't have enough funds", "Cancel");

                        }
                    }
                }

            }
        }

        public void UpdateFund(double newfund)
        {

            Fund = 0;
            Con.CreateTable<funds>();
            var StoredFund = Con.Table<funds>().ToList();
            if (StoredFund.Count > 0)
            {
                StoredFund[0].Funds = newfund;
                Con.Update(StoredFund[0]);
                Fund = newfund;
            }
        }
        public void GeFund()
        {

            Fund = 0;
            Con.CreateTable<funds>();
            var StoredFund = Con.Table<funds>().ToList();
            if (StoredFund.Count > 0)
            {
                Fund = StoredFund[0].Funds;
            }
            else
            {
                funds nfund = new funds
                {
                    Funds = 80000
                };
                Con.Insert(nfund);
                Fund = 80000;


            }

        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;

            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }

    }
}
