using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoClient.Data.Storages;
using CryptoClient.Stores;

namespace CryptoClient.ViewModels
{
    internal class DetailsViewModel : ObservableObject
    {
        private SelectedModelStore _selectedModelStore;

        private string title;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private Dictionary<DateTime, double> chartData;
        public Dictionary<DateTime, double> ChartData
        {
            get => chartData;
            set => SetProperty(ref chartData, value);
        }

        private Dictionary<string, double> markets;
        public Dictionary<string, double> Markets
        {
            get => markets;
            set => SetProperty(ref markets, value);
        }

        private string price;
        public string Price
        {
            get { return price; }
            set => SetProperty(ref price, value);
        }

        private string changePercent;
        public string ChangePercent
        {
            get { return changePercent; }
            set => SetProperty(ref changePercent, value);
        }

        private string link;
        public string Link
        {
            get { return link; }
            set => SetProperty(ref link, value);
        }

        public SolidColorBrush Color => ChangePercent[0] == '-' ?
            new SolidColorBrush(Colors.Red) :
            new SolidColorBrush(Colors.Green);

        public DetailsViewModel(
            SelectedModelStore selectedModelStore,
            StorageService storageService)
        {
            _selectedModelStore = selectedModelStore;

            selectedModelStore.SelectedModelChanged +=
            SelectedModelStore_SelectedModelChanged;
        }

        private void SelectedModelStore_SelectedModelChanged()
        {
            ChartData = _selectedModelStore.SelectedModel.History;
            Markets = _selectedModelStore.SelectedModel.Markets;
            Title = _selectedModelStore.SelectedModel.Name;
            Price = _selectedModelStore.SelectedModel.Price.ToString() + " $";
            ChangePercent = _selectedModelStore.SelectedModel.ChangePercent.ToString() + "%";
            Link = _selectedModelStore.SelectedModel.Link;
        }
    }
}
