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

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }
        private string text = "1";

        public string Result
        {
            get => result;
            set => SetProperty(ref result, value);
        }
        private string result;

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
                Result = Math.Round(FirstCurrency.Price / SecondCurrency.Price * Convert.ToInt32(Text), 2).ToString();
            });
        }

    }
}
