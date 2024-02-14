using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapApp.Models;

namespace MapApp.Helpers
{
    public class FirebaseHelper
    {
        // Replace the following firebase path to your own Firebase Realtime database
        FirebaseClient firebase = new FirebaseClient("https://intense-music-410312-default-rtdb.asia-southeast1.firebasedatabase.app/");

        public FirebaseHelper()
        {
        }

        public async Task<List<Toilet>> GetAllToilets()
        {
            return (await firebase
              .Child("Toilet")
              .OnceAsync<Toilet>()).Select(item => new Toilet
              {
                  NameE = item.Object.NameE,
                  DistrictId = item.Object.DistrictId,
                  AddressE = item.Object.AddressE,
                  OpenHrE = item.Object.OpenHrE,
                  MapCoordinate = item.Object.MapCoordinate,
                  RemarksE = item.Object.RemarksE
              }).ToList();
        }
    }
}
