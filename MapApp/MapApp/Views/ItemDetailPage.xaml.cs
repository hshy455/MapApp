using MapApp.Models;
using MapApp.Resources;
using MapApp.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MapApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            var viewModel = new ItemDetailViewModel();
            BindingContext = viewModel;
            Console.WriteLine(Data.SelectedToilet.Position.Latitude + "," + Data.SelectedToilet.Position.Longitude);
            Map map = new Map(MapSpan.FromCenterAndRadius(Data.SelectedToilet.Position, Distance.FromKilometers(1)));
            map.IsShowingUser = true;
            Content = map;
            Pin pin = new Pin
            {
                Label = Data.SelectedToilet.NameE,
                Address = Data.SelectedToilet.AddressE,
                Type = PinType.Place,
                Position = Data.SelectedToilet.Position
            };
            map.Pins.Add(pin);
        }

    }
}