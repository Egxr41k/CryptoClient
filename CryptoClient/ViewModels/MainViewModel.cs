using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
