using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using MapApp.Resources;
using System.Net.Sockets;

namespace MapApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
            Console.WriteLine(Data.UserPosition.Longitude);
            Map map = new Map(MapSpan.FromCenterAndRadius(Data.UserPosition, Distance.FromKilometers(1)));
            map.IsShowingUser = true;
            Content = map;
            Data.Toilets.ToList().ForEach(Toilet =>
            {
                Pin pin = new Pin
                {
                    Label = Toilet.NameE,
                    Address = Toilet.AddressE,
                    Type = PinType.Place,
                    Position = Toilet.Position
                };
                
                map.Pins.Add(pin);
                pin.InfoWindowClicked += async (s, args) =>
                {
                    string pinName = ((Pin)s).Label;
                    await DisplayAlert(pinName, $"The distance between you and the toilet is {Distance.BetweenPositions(Data.UserPosition,Toilet.Position).Meters}m.", "Ok");
                };
            });
            Circle circle = new Circle
                {
                    Center = Data.UserPosition,
                    Radius = new Distance(250),
                    StrokeColor = Color.FromHex("#88FF0000"),
                    StrokeWidth = 8,
                    FillColor = Color.FromHex("#88FFC0CB")
                };
                map.MapElements.Add(circle);
        }
    }
}