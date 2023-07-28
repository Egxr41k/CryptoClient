using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoClient.Models;
using CryptoClient.Stores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace CryptoClient.ViewModels
{
    internal class DetailsViewModel : ObservableObject
    {
        public ICommand SearchCommand { get; set; }

        private string title;
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
        private Dictionary<string, double> markets;
        public Dictionary<string, double> Markets
        {
            get => markets;
            set => SetProperty(ref markets, value);
        }

        private string errorMsg;
        public string ErrorMsg
        {
            get => errorMsg;
            set => SetProperty(ref errorMsg, value);
        }

        public string TextBoxContent
        {
            get => _textBoxContent;
            set => SetProperty(ref _textBoxContent, value);
        }

        private string _textBoxContent;

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
                model.Markets = await JsonService.GetMarketsAsync(model.Name);

                SelectedModelStore.SelectedModel = model;
                ErrorMsg = string.Empty;
            }
            else ErrorMsg = "no results found";
        }

        private void SelectedModelStore_SelectedModelChanged()
        {
            ChartData = SelectedModelStore.SelectedModel.History;
            Markets = SelectedModelStore.SelectedModel.Markets;
            Title = SelectedModelStore.SelectedModel.Name;
            Price = SelectedModelStore.SelectedModel.Price.ToString() + " $";
            ChangePercent = SelectedModelStore.SelectedModel.ChangePercent.ToString() + "%";
            Link = SelectedModelStore.SelectedModel.Link;

        }
    }
}
