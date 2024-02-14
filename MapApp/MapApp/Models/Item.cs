using Newtonsoft.Json;
using System;
using Xamarin.Forms.Maps;

namespace MapApp.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public Toilet Toilet { get; set; }

    }
}