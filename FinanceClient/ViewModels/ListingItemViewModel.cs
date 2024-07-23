using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using FinanceClient.Data.Models;
using FinanceClient.Stores;

namespace FinanceClient.ViewModels
{
    internal class ListingItemViewModel : ObservableObject
    {
        private FinanceClientStore cryptoClientStore;

        public CurrencyModel CurrencyModel { get; private set; }

        public SolidColorBrush Color => CurrencyPercent[0] == '-' ?
            new SolidColorBrush(Colors.Red) :
            new SolidColorBrush(Colors.Green);
        public string CurrencyName => CurrencyModel.Symbol;
        public string CurrencyPercent => CurrencyModel.ChangePercent.ToString() +"%";

        public ListingItemViewModel(
            CurrencyModel model, 
            FinanceClientStore cryptoClientStore)
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
