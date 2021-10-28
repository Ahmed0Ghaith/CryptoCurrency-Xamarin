using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCurrencyXamarin.Models
{
    public class CryptoTableModel
    {

        [PrimaryKey]
        public int id { get; set; }
        public string name { get; set; }
        public bool Fav { get; set; }
        public string symbol { get; set; }


    }

    public class Profile
    {

        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string symbol { get; set; }


    }

    public class funds
    {

        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public double Funds { get; set; }



    }

}
