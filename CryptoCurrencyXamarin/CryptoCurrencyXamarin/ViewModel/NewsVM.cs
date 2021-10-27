using CryptoCurrencyXamarin.Exception;
using CryptoCurrencyXamarin.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CryptoCurrencyXamarin.ViewModel
{
    public class NewsVM : BaseViewModel
    {
        public ObservableCollection<NewsModel.Article> NewsList { get; set; }

        public Command<NewsModel.Article> ArticleTabbed { get; set; }
        public NewsVM()
        {
            NewsList = new ObservableCollection<NewsModel.Article>();
            ArticleTabbed = new Command<NewsModel.Article>(Tabbed);
            var Result = GetNews();
            if (Result.articles.Count > 0)
            {
                foreach (var Item in Result.articles)
                {
                    NewsList.Add(Item);

                }
            }
        }

        private async void Tabbed(NewsModel.Article obj)
        {
            await Browser.OpenAsync(obj.url, BrowserLaunchMode.SystemPreferred);

        }

        NewsModel.Root GetNews()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                // await App.Current.MainPage.DisplayAlert("Error", "Internet Connection Failed", "Cancel");
                throw new InternetConnectionException("Internet Connection Failed");

            }
            else
            {

                var client = new RestClient("https://newsapi.org/v2/everything?domains=coindesk.com&apiKey=9337764e1ed84136b0701d5a2985b0b7");
                var request = new RestRequest(Method.GET);
                request.AddHeader("postman-token", "d767f2f1-79f8-18d8-30c8-dd275491838f");
                request.AddHeader("cache-control", "no-cache");
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    string json = string.Empty;
                    json = response.Content;
                    //Deserialize Json
                    return JsonConvert.DeserializeObject<NewsModel.Root>(json);
                }
                else
                {
                    throw response.ErrorException;

                }
            }






        }
    }
}
