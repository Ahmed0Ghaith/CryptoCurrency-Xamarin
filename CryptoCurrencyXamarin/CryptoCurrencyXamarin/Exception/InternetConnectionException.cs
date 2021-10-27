


namespace CryptoCurrencyXamarin.Exception
{
    public class InternetConnectionException : System.Exception
    {
        public InternetConnectionException()
        {
        }

        public InternetConnectionException(string message) : base(message)
        {

        }

        public InternetConnectionException(string message, System.Exception inner) : base(message, inner)
        {
        }
    }
}
