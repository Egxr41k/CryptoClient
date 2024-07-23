using CommunityToolkit.Mvvm.ComponentModel;

namespace FinanceClient.ViewModels
{
    internal class MainViewModel : ObservableObject
    {
        public FinanceClientViewModel FinanceClientViewModel { get; set; }
        public MainViewModel(FinanceClientViewModel cryptoClientViewModel)
        {
            FinanceClientViewModel = cryptoClientViewModel;
        }
    }
}
