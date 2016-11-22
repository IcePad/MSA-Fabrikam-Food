using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Moodify.Views {
    public partial class MapPage : ContentPage {
        public MapPage() {
            InitializeComponent();

            var map = new Map(
            MapSpan.FromCenterAndRadius(
                    new Position(-36.9196680, 174.7488320), Distance.FromMiles(0.3))) {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            Content = stack;

            var position = new Position(-36.9196680, 174.7488320); // Latitude, Longitude
            var pin = new Pin {
                Type = PinType.Place,
                Position = position,
                Label = "Fabrikam Food",
                Address = "28 Oakdale Rd"
            };
            map.Pins.Add(pin);

        }

    }
}
