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
}
