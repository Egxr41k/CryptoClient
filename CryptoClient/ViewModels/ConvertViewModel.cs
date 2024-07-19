using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoClient.Data.Services;
using CryptoClient.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoClient.ViewModels
{
    internal class ConvertViewModel : ObservableObject
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


        private string _firstCurrency;
        public string FirstCurrency
        {
            get => _firstCurrency;
            set => SetProperty(ref _firstCurrency, value);
        }

        private string _secondCurrency;
        public string SecondCurrency
        {
            get => _secondCurrency;
            set => SetProperty(ref _secondCurrency, value);
        }

        public Dictionary<string, double> AllowedCurrencies { get; set; }

        public ConvertViewModel(IApiClient apiService)
        {
            AllowedCurrencies = apiService.GetCurrenciesCoastsAsync().GetAwaiter().GetResult();

            ConvertCommand = new RelayCommand(() =>
            {
                try
                {
                    var count = Convert.ToInt32(Count);
                    var fcurrency = AllowedCurrencies[FirstCurrency];
                    var scurrency = AllowedCurrencies[SecondCurrency];

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
