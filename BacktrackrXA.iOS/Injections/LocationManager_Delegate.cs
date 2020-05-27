using System;
using System.Linq;
using BacktrackrXA.Injections;
using BacktrackrXA.iOS.Extensions;
using CoreLocation;
using Foundation;

namespace BacktrackrXA.iOS.Injections
{
    public class LocationManager_Delegate : CoreLocation.CLLocationManagerDelegate
    {
        private LocationManager_iOS _parent { get; set; }
        public LocationManager_Delegate(LocationManager_iOS parent)
        {
            _parent = parent;
        }

        [Export("locationManager:didUpdateLocations:")]
        public override void LocationsUpdated(CLLocationManager manager, CLLocation[] locations)
        {
            _parent.TriggerLocationUpdate(locations);
        }
    }
}
