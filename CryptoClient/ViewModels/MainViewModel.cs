using CommunityToolkit.Mvvm.ComponentModel;

namespace CryptoClient.ViewModels
{
    internal class MainViewModel : ObservableObject
    {
        public CryptoClientViewModel CryptoClientViewModel { get; set; }
        public MainViewModel(CryptoClientViewModel cryptoClientViewModel)
        {
            CryptoClientViewModel = cryptoClientViewModel;
        }
    }
}
