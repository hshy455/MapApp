using MapApp.Helpers;
using MapApp.Models;
using MapApp.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MapApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenMapCommand = new Command(async () => await Shell.Current.GoToAsync($"//{nameof(MapPage)}"));
        }

        public ICommand OpenMapCommand { get; }

        
    }
}