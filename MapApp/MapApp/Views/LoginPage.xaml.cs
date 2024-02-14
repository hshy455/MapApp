using MapApp.Helpers;
using MapApp.Models;
using MapApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MapApp.Resources;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using System.Threading;

namespace MapApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        public bool done = false;

        public LoginPage()
        {
            
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
            LoginButton.IsEnabled = false;
            getAllToiletsAsync();
            GetCurrentLocation();
        }

        public async Task getAllToiletsAsync()
        {
            FirebaseHelper fbhelper = new FirebaseHelper();
            var allToilets = await fbhelper.GetAllToilets();
            Console.WriteLine("Retrieving Toilet Info ....................................................................... ");
            Console.WriteLine("Number of Toilets: " + allToilets.Count);
            Data.Toilets = allToilets;
            
            Console.WriteLine("location" + done);
            if (done == true)
            {
                ProgressLabel.Text = "Finished 100%";
                Progressbar.Progress = 1;
                LoginButton.IsEnabled = true;
            }
            else
            {
                done = true;
                ProgressLabel.Text = "Loding Data 50%";
                Progressbar.Progress = 0.5;
            }

        }

        CancellationTokenSource cts;

        async Task GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    Data.UserPosition= new Position(location.Latitude, location.Longitude);
                    Console.WriteLine("database" + done);
                    if (done == true)
                    {
                        ProgressLabel.Text = "Finished 100%";
                        Progressbar.Progress = 1;
                        LoginButton.IsEnabled = true;
                    }
                    else
                    {
                        done = true;
                        ProgressLabel.Text = "Loding Data 50%";
                        Progressbar.Progress = 0.5;
                    }
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }

        protected override void OnDisappearing()
        {
            if (cts != null && !cts.IsCancellationRequested)
                cts.Cancel();
            base.OnDisappearing();
        }

    }
}