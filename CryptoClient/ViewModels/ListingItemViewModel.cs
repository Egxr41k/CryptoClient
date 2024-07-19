using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CryptoClient.Data.Models;
using CryptoClient.Stores;

namespace CryptoClient.ViewModels
{
    internal class ListingItemViewModel : ObservableObject
    {
        private CryptoClientStore cryptoClientStore;

        public CurrencyModel CurrencyModel { get; private set; }

        public SolidColorBrush Color => CurrencyPercent[0] == '-' ?
            new SolidColorBrush(Colors.Red) :
            new SolidColorBrush(Colors.Green);
        public string CurrencyName => CurrencyModel.Symbol;
        public string CurrencyPercent => CurrencyModel.ChangePercent.ToString() +"%";

        public ListingItemViewModel(
            CurrencyModel model, 
            CryptoClientStore cryptoClientStore)
        {
            CurrencyModel = model;
        }

        internal void Update(CurrencyModel model)
        {
            CurrencyModel = model;
            OnPropertyChanged(nameof(CurrencyModel));
        }
    }
}
