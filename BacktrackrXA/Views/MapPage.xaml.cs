using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BacktrackrXA.Injections;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace BacktrackrXA.Views
{
    [DesignTimeVisible(false)]
    public partial class MapPage : ContentPage
    {
        private bool HasSetUserLocation = false;

        public static BindableProperty IsFollowingUserProperty = BindableProperty.Create(
            propertyName: nameof(IsFollowingUser),
            declaringType: typeof(MapPage),
            returnType: typeof(bool),
            defaultValue: false
        );

        public bool IsFollowingUser
        {
            get => (bool)GetValue(IsFollowingUserProperty);
            set => SetValue(IsFollowingUserProperty, value);
        }

        public static BindableProperty ZoomLevelProperty = BindableProperty.Create(
           propertyName: nameof(ZoomLevel),
           declaringType: typeof(MapPage),
           returnType: typeof(int),
           defaultValue: 5
       );

        public int ZoomLevel
        {
            get => (int)GetValue(ZoomLevelProperty);
            set => SetValue(ZoomLevelProperty, value);
        }


        public MapPage()
        {
            Title = "Map";
            InitializeComponent();

            ToolbarItems.Add(new ToolbarItem("Settings", null, () => Navigation.PushAsync(new SettingsPage())));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.LocationManager.LocationUpdated += Manager_LocationUpdated;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            App.LocationManager.LocationUpdated -= Manager_LocationUpdated;
        }

        double GetLatLngDeg() => 360 / (Math.Pow(2, ZoomLevel));


        private void Manager_LocationUpdated(object sender, LocationManagerUpdate e)
        {
            if (!HasSetUserLocation || IsFollowingUser)
            {
                ZoomLevel = ZoomLevel >= 15 ? ZoomLevel : 15;
                var latlongdeg = GetLatLngDeg();
                var span = new MapSpan(new Position(e.NewLocation.Coordinate.Latitude, e.NewLocation.Coordinate.Longitude), latlongdeg, latlongdeg);
                Map.MoveToRegion(span);
                HasSetUserLocation = true;
            }
        }

        void LocationButton_Clicked(object sender, EventArgs e)
        {
            ZoomLevel = ZoomLevel >= 15 ? ZoomLevel : 15;
            IsFollowingUser = !IsFollowingUser;
            LocationButton.FontFamily = IsFollowingUser ? Constants.FontAwesomeSolid : Constants.FontAwesomeRegular;

            var lastLocation = App.LocationManager.LastLocation;
            var latlongdeg = GetLatLngDeg();
            var span = new MapSpan(new Position(lastLocation.Coordinate.Latitude, lastLocation.Coordinate.Longitude), latlongdeg, latlongdeg);
            Map.MoveToRegion(span);
        }

        void ZoomInButton_Clicked(object sender, EventArgs e)
        {
            ZoomLevel += 1;
            var latlongdeg = GetLatLngDeg();
            Map.MoveToRegion(new MapSpan(Map.VisibleRegion.Center, latlongdeg, latlongdeg));
        }

        void ZoomOutButton_Clicked(object sender, EventArgs e)
        {
            ZoomLevel -= 1;
            var latlongdeg = GetLatLngDeg();
            Map.MoveToRegion(new MapSpan(Map.VisibleRegion.Center, latlongdeg, latlongdeg));
        }

        void LayerButton_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new MapTypePopover());
        }
    }
}
