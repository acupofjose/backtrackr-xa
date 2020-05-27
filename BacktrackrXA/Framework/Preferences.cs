using System;
namespace BacktrackrXA.Framework
{
    public static class Preferences
    {
        public static string ServerHost
        {
            get => Xamarin.Essentials.Preferences.Get(nameof(ServerHost), null);
            set => Xamarin.Essentials.Preferences.Set(nameof(ServerHost), value);
        }

        public static int ServerPort
        {
            get => Xamarin.Essentials.Preferences.Get(nameof(ServerPort), int.MinValue);
            set => Xamarin.Essentials.Preferences.Set(nameof(ServerPort), value);
        }

        public static bool ServerUseWebsockets
        {
            get => Xamarin.Essentials.Preferences.Get(nameof(ServerUseWebsockets), true);
            set => Xamarin.Essentials.Preferences.Set(nameof(ServerUseWebsockets), value);
        }
    }
}
