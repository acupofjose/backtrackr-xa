using System;
using System.Collections.Generic;
using BacktrackrXA.Framework;
using Xamarin.Forms;

namespace BacktrackrXA.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            ToolbarItems.Add(new ToolbarItem("Save", null, Save));

            ServerHost.Text = Preferences.ServerHost;
            ServerPort.Text = Preferences.ServerPort > 0 ? Preferences.ServerPort.ToString() : null;
            ServerUseWebsockets.On = Preferences.ServerUseWebsockets;
        }

        private void Save()
        {
            Preferences.ServerHost = ServerHost.Text;
            Preferences.ServerPort = int.Parse(ServerPort.Text);
            Preferences.ServerUseWebsockets = ServerUseWebsockets.On;
        }
    }
}
