using System;
using Newtonsoft.Json;

namespace BacktrackrXA.Injections
{
    public interface ILocationManager
    {
        Location LastLocation { get; set; }
        event EventHandler<LocationManagerUpdate> LocationUpdated;
        void StartUpdatingLocation();
        void StopUpdatingLocation();
        void RequestPermissions(PermissionType permissionType = PermissionType.Always);
    }

    public enum PermissionType
    {
        Always,
        WhenInUse
    }

    public class LocationManagerUpdate : EventArgs
    {
        public Location NewLocation { get; set; }
        public Location OldLocation { get; set; }
    }

    public class Location
    {
        [JsonProperty("altitude")]
        public double Altitude { get; set; }

        [JsonProperty("coordinate")]
        public LocationCoordinate2D Coordinate { get; set; }

        [JsonProperty("course")]
        public double Course { get; set; }

        [JsonProperty("courseAccuracy")]
        public double CourseAccuracy { get; set; }

        [JsonProperty("horizontalAccuracy")]
        public double HorizontalAccuracy { get; set; }

        [JsonProperty("speed")]
        public double Speed { get; set; }

        [JsonProperty("speedAccuracy")]
        public double SpeedAccuracy { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("verticalAccuracy")]
        public double VerticalAccuracy { get; set; }
    }

    public class LocationCoordinate2D
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }
}
