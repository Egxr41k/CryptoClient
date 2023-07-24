using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoClient.ViewModels
{
    public class HomeViewModel : ObservableObject
    {
        public string Text { get; set; } = "HomeView";
    }
}
