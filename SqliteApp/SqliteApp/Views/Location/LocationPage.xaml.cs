using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;

namespace SqliteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationPage : ContentPage
    {
        public LocationPage()
        {
            InitializeComponent();
            Init();
        }



        private async void Init()
        {
            var map = await InitMap();

            var grid = InitGrid();

            grid.Children.Add(map, 0, 0);

            var panel = await GetPanel();

            grid.Children.Add(panel, 0, 1);

            Content = grid;

            await UpdateCordinate(panel, map);

        }

        private async Task UpdateCordinate(Grid panel, Map map)
        {
            while (true)
            {
                await Task.Delay(5000);

                var position = await GetPosition();

                var longth = position.Longitude;
                var width = position.Latitude;

                var coordinate = (Label)panel.Children[1];

                coordinate.Text = $"Latitude: {width}; Longitude: {longth}";

                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude),
                                             Distance.FromMiles(0.7)));
            }
        }

        private async Task<Map> InitMap()
        {
            var position = await GetPosition();

            var longth = position.Longitude;

            var width = position.Latitude;

            Position cubic = new Position(width, longth);
            MapSpan mapSpan = new MapSpan(cubic, 0.01, 0.01);

            Map map = new Map(mapSpan)
            {
                IsShowingUser = true,
                Scale = 1
            };

            return map;
        }

        private Grid InitGrid()
        {
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0.8, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0.2, GridUnitType.Star) });

            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });//{Width = GridLength.Auto});

            return grid;
        }

        private async Task<Grid> GetPanel()
        {
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Star });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Star });

            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });

            grid.Children.Add(new Label() { Text = "Current coordinate: " }, 0, 0);


            var position = await GetPosition();
            var longth = position.Longitude;
            var width = position.Latitude;

            grid.Children.Add(new Label() { Text = $"Latitude: {width}; Longitude: {longth}" }, 1, 0);

            Button getCurrentLocationButton = new Button() { Text = "Get current location" };
            getCurrentLocationButton.Clicked += GetCurrentLocationButton_click;

            grid.Children.Add(getCurrentLocationButton, 0, 1);

            Geocoder geoCoder = new Geocoder();

            Position positionAdress = new Position(width, longth);
            IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(positionAdress);
            string address = possibleAddresses.FirstOrDefault();

            grid.Children.Add(new Label() { Text = address }, 1, 1);

            return grid;
        }


        private async void GetCurrentLocationButton_click(object sender, EventArgs args)
        {
            var position = await GetPosition();
            var longth = position.Longitude;
            var width = position.Latitude;

            Geocoder geoCoder = new Geocoder();
            Position positionAdress = new Position(width, longth);
            IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(positionAdress);
            string address = possibleAddresses.FirstOrDefault();

            var content = (Grid)Content;

            var panel = (Grid)content.Children[1];

            var label = (Label)panel.Children[3];

            label.Text = address;
        }

        private async Task<Plugin.Geolocator.Abstractions.Position> GetPosition()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync(new TimeSpan(10000));

            return position;
        }

        async void Search_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new FindByPositionPage()));
        }
    }
}