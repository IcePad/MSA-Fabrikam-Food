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
            //Create the map instance to a specified location
            var map = new Map(
            MapSpan.FromCenterAndRadius(
                    new Position(-36.8546600, 174.7673700), Distance.FromMiles(0.3))) {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            Content = stack;

            //Add pin to the position
            var position = new Position(-36.8546600, 174.7673700); // Latitude, Longitude
            var pin = new Pin {
                Type = PinType.Place,
                Position = position,
                Label = "Fabrikam Food",
                Address = "1 Mount Street"
            };
            map.Pins.Add(pin);

        }

    }
}
