using CommunityToolkit.Mvvm.ComponentModel;
using CryptoClient.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoClient.ViewModels
{
    internal class DetailsViewModel : ObservableObject
    {
        private SelectedModelStore selectedListAppModelStore;

        public DetailsViewModel(SelectedModelStore selectedListAppModelStore)
        {
            this.selectedListAppModelStore = selectedListAppModelStore;
        }
    }
}
