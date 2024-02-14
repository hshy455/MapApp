using MapApp.Models;
using MapApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MapApp.Resources;
using System.Linq;
using Xamarin.Forms.Maps;

namespace MapApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
       

        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            Console.WriteLine(Data.Toilets.Count);
            Data.Toilets.ToList().ForEach(Toilet =>
            {
                Toilet.Latitude = double.Parse(Toilet.MapCoordinate.Split(',')[0]);
                Toilet.Longitude = double.Parse(Toilet.MapCoordinate.Split(',')[1]);
                Toilet.Position = new Position(Toilet.Latitude, Toilet.Longitude);
                /*
                Console.WriteLine("Toilet Name: " + Toilet.NameE);
                Console.WriteLine($"District: {Toilet.DistrictId}");
                Console.WriteLine($"Address: {Toilet.AddressE}");
                Console.WriteLine($"Opening Hour: {Toilet.OpenHrE}");
                Console.WriteLine($"Latitude: {Toilet.Latitude}");
                Console.WriteLine($"Longitude: {Toilet.Longitude}");
                Console.WriteLine($"Remarks: {Toilet.RemarksE}");
                Console.WriteLine("======================================================");
                */

            }
            );
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
