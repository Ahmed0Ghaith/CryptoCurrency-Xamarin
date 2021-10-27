using CryptoCurrencyXamarin.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CryptoCurrencyXamarin.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {


        public static List<CCInformationModel.Datum> CryptoStorelist { get; set; }



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
