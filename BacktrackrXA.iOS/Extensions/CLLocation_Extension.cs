using System;
using BacktrackrXA.Injections;
using CoreLocation;

namespace BacktrackrXA.iOS.Extensions
{
    public static class CLLocation_Extension
    {
        public static Location ToLocation(this CLLocation instance)
        {
            return new Location
            {
                Altitude = instance.Altitude,
                Coordinate = new LocationCoordinate2D
                {
                    Latitude = instance.Coordinate.Latitude,
                    Longitude = instance.Coordinate.Longitude
                },
                Course = instance.Course,
                CourseAccuracy = instance.CourseAccuracy,
                HorizontalAccuracy = instance.HorizontalAccuracy,
                Speed = instance.Speed,
                SpeedAccuracy = instance.SpeedAccuracy,
                Timestamp = (DateTime)instance.Timestamp,
                VerticalAccuracy = instance.VerticalAccuracy
            };
        }
    }
}
