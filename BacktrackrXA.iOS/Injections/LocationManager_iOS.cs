using System;
using System.Linq;
using BacktrackrXA.Injections;
using BacktrackrXA.iOS.Extensions;
using BacktrackrXA.iOS.Injections;
using CoreLocation;
using Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocationManager_iOS))]
namespace BacktrackrXA.iOS.Injections
{
    public class LocationManager_iOS : ILocationManager
    {
        private CLLocationManager _locationManager;

        public Location LastLocation { get; set; }

        public IntPtr Handle => _locationManager.Handle;

        private bool _hasInitialLocation = false;
        public event EventHandler<LocationManagerUpdate> LocationUpdated;

        public LocationManager_iOS()
        {
            _locationManager = new CLLocationManager
            {
                PausesLocationUpdatesAutomatically = false,
                AllowsBackgroundLocationUpdates = true,
                DistanceFilter = 15,
                Delegate = new LocationManager_Delegate(this),
                DesiredAccuracy = CLLocation.AccuracyBest,
                ActivityType = CLActivityType.OtherNavigation,
                ShowsBackgroundLocationIndicator = false
            };
            _locationManager.AllowDeferredLocationUpdatesUntil(10, 120);
        }

        public void TriggerLocationUpdate(CLLocation[] coreLocations)
        {
            foreach (var coreLocation in coreLocations)
            {
                var args = new LocationManagerUpdate
                {
                    NewLocation = coreLocation.ToLocation(),
                    OldLocation = LastLocation
                };
                LocationUpdated?.Invoke(this, args);
                App.LocationUpdated(this, args);
                LastLocation = coreLocation.ToLocation();
            }
        }

        public void StartUpdatingLocation()
        {
            _locationManager.StartUpdatingLocation();

            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                if (_locationManager.Location != null)
                {
                    LastLocation = _locationManager.Location.ToLocation();
                    LocationUpdated?.Invoke(this, new LocationManagerUpdate { NewLocation = LastLocation });
                    _hasInitialLocation = true;
                }
                return !_hasInitialLocation;
            });
        }

        public void StopUpdatingLocation() => _locationManager.StopUpdatingLocation();

        public void RequestPermissions(PermissionType permissionType = PermissionType.Always)
        {
            switch (permissionType)
            {
                case PermissionType.Always:
                    _locationManager.RequestAlwaysAuthorization();
                    break;
                case PermissionType.WhenInUse:
                    _locationManager.RequestWhenInUseAuthorization();
                    break;
            }
        }

        public void Dispose() => _locationManager.Dispose();
    }
}
