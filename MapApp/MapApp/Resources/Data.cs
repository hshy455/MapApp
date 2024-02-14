using MapApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MapApp.Resources
{
    internal class Data
    {
        public static List<Toilet> Toilets {  get; set; }

        public static Position UserPosition { get; set; }

        public static Toilet SelectedToilet { get; set; }
    }
}
