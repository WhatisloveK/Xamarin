using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Map = Xamarin.Forms.Maps.Map;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;

namespace SqliteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindByPositionPage : ContentPage
    {
        public FindByPositionPage()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            var grid = GetGrid();

            var map = GetMap();

            var panel = GetPanel();

            grid.Children.Add(map, 0, 0);
            grid.Children.Add(panel, 0, 1);

            Content = grid;
        }

        private Grid GetGrid()
        {
            var grid = new Grid();

            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0.75, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0.25, GridUnitType.Star) });

            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });//{Width = GridLength.Auto});

            return grid;
        }

        private Map GetMap()
        {
            Position cubic = new Position(50.383203, 30.471007); //cubic)
            MapSpan mapSpan = new MapSpan(cubic, 0.01, 0.01);

            Map map = new Map(mapSpan)
            {
                IsShowingUser = true,
                Scale = 1
            };

            return map;
        }

        private Grid GetPanel()
        {
            var grid = new Grid();

            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });

            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Star });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Star });

            grid.Children.Add(new Entry(), 0, 0);
            grid.Children.Add(new Entry(), 0, 1);

            var findButton = new Button() { Text = "Find" };
            findButton.Clicked += FindButton_click;

            grid.Children.Add(findButton, 1, 0);


            var distButton = new Button() { Text = "Calculate distance" };
            distButton.Clicked += DistButton_click;

            grid.Children.Add(distButton, 1, 1);


            return grid;
        }

        private async void FindButton_click(object sender, EventArgs args)
        {
            var content = (Grid)Content;

            var map = (Map)content.Children[0];
            var panel = (Grid)content.Children[1];

            var entryWidth = (Entry)panel.Children[0];
            var entryLong = (Entry)panel.Children[1];

            if (double.TryParse(entryWidth.Text, out double width) && double.TryParse(entryLong.Text, out double length))
            {
                Geocoder geoCoder = new Geocoder();
                Position positionAdress = new Position(width, length);
                IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(positionAdress);
                string address = possibleAddresses.FirstOrDefault();

                var pos = new Position(width, length);

                Pin pin = new Pin
                {
                    Label = address,
                    Type = PinType.Place,
                    Position = pos
                };

                map.Pins.Clear();
                map.Pins.Add(pin);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromMiles(0.7)));
            }

        }

        private async void DistButton_click(object sender, EventArgs args)
        {
            FindButton_click(sender, args);

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync(new TimeSpan(10000));

            var myPosition = new Position(position.Latitude, position.Longitude);

            var content = (Grid)Content;

            var map = (Map)content.Children[0];
            var panel = (Grid)content.Children[1];

            var entryWidth = (Entry)panel.Children[0];
            var entryLong = (Entry)panel.Children[1];

            Position positionAdress;

            if (double.TryParse(entryWidth.Text, out double width) && double.TryParse(entryLong.Text, out double length))
            {
                positionAdress = new Position(width, length);
            }
            else
            {
                throw new Exception("Incorrect data");
            }

            Polyline polyline = new Polyline
            {
                StrokeColor = Color.Blue,
                StrokeWidth = 12,
                Geopath =
                {
                    myPosition,
                    positionAdress
                }
            };

            var dist = Location.CalculateDistance(myPosition.Latitude, myPosition.Longitude, positionAdress.Latitude, positionAdress.Longitude, DistanceUnits.Kilometers);

            map.MapElements.Add(polyline);

            await DisplayAlert("Info", $"Distance: {dist} km.", "Ok");
        }
    }
}