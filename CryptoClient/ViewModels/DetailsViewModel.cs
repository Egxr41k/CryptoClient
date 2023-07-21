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
        private readonly SelectedModelStore _selectedModelStore;

        private string title = "Home Page";
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }


        public DetailsViewModel(SelectedModelStore selectedModelStore)
        {
            Title = "Home Page";
            _selectedModelStore = selectedModelStore;
            _selectedModelStore.SelectedModelChanged +=
            _selectedModelStore_SelectedModelChanged;
        }

        private void _selectedModelStore_SelectedModelChanged()
        {
            Title = _selectedModelStore.SelectedModel?.Name ?? "SelectedModel is null";
        }
    }
}
