using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoClient.ViewModels
{
    public class SearchViewModel : ObservableObject
    {
        public ICommand SearchCommand { get; set; }
        public string TextBoxContent
        {
            get => _textBoxContent;
            set
            {
                SetProperty(ref _textBoxContent, value);
            }
        }
        private string _textBoxContent;
        public SearchViewModel()
        {
            SearchCommand = new RelayCommand(async () =>
            {
                DisplayCurrency( await JsonService.SearchAsync(TextBoxContent));
            });
        }
        private void DisplayCurrency(CurrencyModel? moodel)
        {
            if(moodel != null)
            {
                //show model details
            }
            else
            {
                // no results found
            }
        }
    }
}
