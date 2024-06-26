﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoClient.Contracts;
using CryptoClient.Models;
using CryptoClient.Services;
using CryptoClient.Stores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace CryptoClient.ViewModels
{
    internal class DetailsViewModel : ObservableObject
    {
        private SelectedModelStore _selectedModelStore;
        private IJsonService _jsonService;
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

        public DetailsViewModel(
            SelectedModelStore selectedModelStore, 
            IJsonService jsonService)
        {
            _selectedModelStore = selectedModelStore;
            _jsonService = jsonService;

            selectedModelStore.SelectedModelChanged +=
            SelectedModelStore_SelectedModelChanged;

            SearchCommand = new RelayCommand(async () =>
            {
                try
                {
                    CurrencyModel model =  await jsonService.SearchAsync(TextBoxContent);
                    selectedModelStore.SelectedModel = model;
                }
                catch
                {
                    ErrorMsg = "no results found";
                }
            });
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
