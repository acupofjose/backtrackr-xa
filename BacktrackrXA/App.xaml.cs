using System;
using BacktrackrXA.Framework;
using BacktrackrXA.Injections;
using BacktrackrXA.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BacktrackrXA
{
    public partial class App : Application
    {
        public static Application Instance => (Application)App.Current;

        public static ILocationManager LocationManager => DependencyService.Get<ILocationManager>();

        public NavigationPage RootNavigationPage { get; private set; }

        public App()
        {
            InitializeComponent();

            RootNavigationPage = new NavigationPage(new MapPage());
            SocketClient.Instance.Connect();

            MainPage = RootNavigationPage;
        }

        protected override void OnStart()
        {
            LocationManager.RequestPermissions(PermissionType.Always);
            LocationManager.StartUpdatingLocation();
        }

        public static async void LocationUpdated(object sender, LocationManagerUpdate e)
        {
            if (!string.IsNullOrEmpty(Preferences.ServerHost) && Preferences.ServerPort > 0)
            {
                if (!SocketClient.Instance.IsStarted)
                    await SocketClient.Instance.Connect();

                SocketClient.Instance.PostLocation(e.NewLocation);
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
