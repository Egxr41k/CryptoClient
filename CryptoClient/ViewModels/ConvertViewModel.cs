using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoClient.ViewModels
{
    public class ConvertViewModel : ObservableObject
    {
        public ICommand ConvertCommand { get; set; }

        public string Count
        {
            get => count;
            set => SetProperty(ref count, value);
        }
        private string count = "1";

        public string Result
        {
            get => result;
            set => SetProperty(ref result, value);
        }
        private string result;

        private string errorMsg;
        public string ErrorMsg
        {
            get => errorMsg;
            set => SetProperty(ref errorMsg, value);
        }


        private CurrencyModel _firstCurrency;
        public CurrencyModel FirstCurrency
        {
            get => _firstCurrency;
            set => SetProperty(ref _firstCurrency, value);
        }

        private CurrencyModel _secondCurrency;
        public CurrencyModel SecondCurrency
        {
            get => _secondCurrency;
            set => SetProperty(ref _secondCurrency, value);
        }

        public List<CurrencyModel> AllowedCurrencies { get; set; }

        public ConvertViewModel()
        {
            AllowedCurrencies = JsonService.GetTopCurrenciesAsync().Result;

            ConvertCommand = new RelayCommand(() =>
            {
                try
                {
                    var count = Convert.ToInt32(Count);
                    var fcurrency = FirstCurrency.Price;
                    var scurrency = SecondCurrency.Price;

                    Result = Math.Round(fcurrency / scurrency * count, 2).ToString();
                    ErrorMsg = string.Empty;

                }
                catch(Exception ex)
                {
                    if(ex.Message == "Object reference not set to an instance of an object.")
                    {
                        ErrorMsg = "Currency isn`t selected";
                    }
                    else if(ex.Message == "Input string was not in a correct format.")
                    {
                        ErrorMsg = "Incorrect input";
                    }
                }

            });
        }

    }
}
