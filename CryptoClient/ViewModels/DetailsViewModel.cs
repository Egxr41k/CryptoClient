using CommunityToolkit.Mvvm.ComponentModel;
using CryptoClient.Stores;
using Newtonsoft.Json.Linq;
using Syncfusion.UI.Xaml.Charts;
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
        public Dictionary<DateTime, int> ChartData
        {
            get => chartData;
            set => SetProperty(ref chartData, value);

        }
        private Dictionary<DateTime, int> chartData;

            
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }
        private string text;

        public DetailsViewModel(SelectedModelStore selectedModelStore)
        {
            Title = "Home Page";
            _selectedModelStore = selectedModelStore;
            _selectedModelStore.SelectedModelChanged +=
            _selectedModelStore_SelectedModelChanged;
        }

        private void _selectedModelStore_SelectedModelChanged()
        {
            Text = string.Empty;
            Title = _selectedModelStore.SelectedModel.Name;
            ChartData = _selectedModelStore.SelectedModel.History;
            
        }
    }
}
