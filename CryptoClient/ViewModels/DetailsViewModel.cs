using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoClient.Models;
using CryptoClient.Stores;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoClient.ViewModels
{
    internal class DetailsViewModel : ObservableObject
    {
        public ICommand SearchCommand { get; set; }

        private string title = "Home Page";
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public Dictionary<DateTime, double> ChartData
        {
            get => chartData;
            set => SetProperty(ref chartData, value);

        }
        private Dictionary<DateTime, double> chartData;

        //public string Text
        //{
        //    get => text;
        //    set => SetProperty(ref text, value);
        //}
        //private string text;

        public string TextBoxContent
        {
            get => _textBoxContent;
            set => SetProperty(ref _textBoxContent, value);
        }

        private string _textBoxContent;

        public DetailsViewModel()
        {
            Title = "Home Page";
            SelectedModelStore.SelectedModelChanged +=
            SelectedModelStore_SelectedModelChanged;

            SearchCommand = new RelayCommand(async () =>
            {
                await DisplayCurrencyAsync(await JsonService.SearchAsync(TextBoxContent));
            });
        }

        private async Task DisplayCurrencyAsync(CurrencyModel? model)
        {
            if (model != null)
            {
                model.History = await JsonService.GetHistoryAsync(model.Name);
                SelectedModelStore.SelectedModel = model;//show model details
            }
            else
            {
                // no results found
            }
        }

        private void SelectedModelStore_SelectedModelChanged()
        {
            ChartData = SelectedModelStore.SelectedModel.History;
            Title = SelectedModelStore.SelectedModel.Name;
        }
    }
}
