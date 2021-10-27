using CryptoCurrencyXamarin.Exception;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace CryptoCurrencyXamarin.Helpers
{
    public static class RestApi
    {

        public static T GetAsync<T>(string resource)
        {



            // URL
            string uri = resource;


            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                // await App.Current.MainPage.DisplayAlert("Error", "Internet Connection Failed", "Cancel");
                throw new InternetConnectionException("Internet Connection Failed");
            }
            else
            {

                // Http Call
                var client = new RestClient(uri);
                client.Timeout = -1;

                var request = new RestRequest(Method.GET);
                request.AddHeader("x-cmc_pro_api_key", "4d696681-a815-44fe-819a-3a2c8d815837");


                IRestResponse response = client.Execute(request);


                if (response.IsSuccessful)
                {
                    string json = string.Empty;
                    json = response.Content;
                    //Deserialize Json
                    return JsonConvert.DeserializeObject<T>(json);
                }
                else
                {
                    throw response.ErrorException;

                }
            }










        }

    }
}
