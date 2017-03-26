using System;

namespace MultiScaleTrajectories.Util
{
    public static class GoogleMaps
    {
        public static int TileSize = 256;
        public static double OriginX, OriginY;
        public static double PixelsPerLonDegree;
        public static double PixelsPerLonRadian;

        static GoogleMaps()
        {
            OriginX = TileSize / 2;
            OriginY = TileSize / 2;
            PixelsPerLonDegree = TileSize / 360.0;
            PixelsPerLonRadian = TileSize / (2 * Math.PI);
        }

        public static double DegreesToRadians(double deg)
        {
            return deg * Math.PI / 180.0;
        }

        public static double RadiansToDegrees(double rads)
        {
            return rads * 180.0 / Math.PI;
        }

        public static double Bound(double value, double min, double max)
        {
            value = Math.Min(value, max);
            return Math.Max(value, min);
        }

        //From Lat, Lon to World Coordinate X, Y.
        public static Coordinate EarthToPixel(double latitude, double longitude)
        {
            double siny = Bound(Math.Sin(DegreesToRadians(latitude)), -.9999, .9999);

            Coordinate c = new Coordinate(0, 0)
            {
                X = OriginX + longitude * PixelsPerLonDegree,
                Y = OriginY + .5 * Math.Log((1 + siny) / (1 - siny)) * -PixelsPerLonRadian
            };

            return c;
        }

        //From World Coordinate X, Y to Lat, Lon.
        public static Coordinate PixelToEarth(double x, double y)
        {
            double latRadians = (y - OriginY) / -PixelsPerLonRadian;

            Coordinate c = new Coordinate(0, 0)
            {
                Longitude = (x - OriginX) / PixelsPerLonDegree,
                Latitude = RadiansToDegrees(Math.Atan(Math.Sinh(latRadians)))
            };

            return c;
        }

        public static Bounds GetBounds(Coordinate center, int zoom, int mapWidth, int mapHeight)
        {
            var scale = Math.Pow(2, zoom);

            var centerWorld = EarthToPixel(center.Latitude, center.Longitude);
            var centerPixel = new Coordinate(0, 0)
            {
                X = centerWorld.X * scale,
                Y = centerWorld.Y * scale
            };

            var NEPixel = new Coordinate(0, 0)
            {
                X = centerPixel.X + mapWidth / 2.0,
                Y = centerPixel.Y - mapHeight / 2.0
            };

            var SWPixel = new Coordinate(0, 0)
            {
                X = centerPixel.X - mapWidth / 2.0,
                Y = centerPixel.Y + mapHeight / 2.0
            };

            var NEWorld = new Coordinate(0, 0)
            {
                X = NEPixel.X / scale,
                Y = NEPixel.Y / scale
            };

            var SWWorld = new Coordinate(0, 0)
            {
                X = SWPixel.X / scale,
                Y = SWPixel.Y / scale
            };

            var NELatLon = PixelToEarth(NEWorld.X, NEWorld.Y);
            var SWLatLon = PixelToEarth(SWWorld.X, SWWorld.Y);

            return new Bounds
            {
                NorthEast = NELatLon,
                SouthWest = SWLatLon
            };
        }

        public static Bounds GetZoomLevel(Coordinate center, int mapWidth, int mapHeight, double desiredWorldWidth, double desiredWorldHeight)
        {
            var centerWorld = EarthToPixel(center.Latitude, center.Longitude);
            var NELatLon = centerWorld.X - desiredWorldWidth / 2;
            var SWLatLon = centerWorld.X - desiredWorldWidth / 2;

            var pixelWidth = EarthToPixel(desiredWorldWidth, 0.0).X;
            var pixelHeight = EarthToPixel(0.0, desiredWorldHeight).Y;


            //var NELatLon = 0;
            //var SWLatLon = 0;
            //var NEWorld = EarthToPixel(NELatLon.X, NELatLon.Y);
            //var SWWorld = EarthToPixel(SWLatLon.X, SWLatLon.Y);

            //var centerWorld = EarthToPixel(center.Latitude, center.Longitude);

            //var northScale = mapWidth * (desiredWorldWidth - centerWorld.X) / 2;
            //var southScale = mapWidth * (desiredWorldWidth - centerWorld.X) / 2;
            //var eastScale = mapWidth * (desiredWorldWidth - centerWorld.X) / 2;
            //var westScale = mapWidth * (desiredWorldWidth - centerWorld.X) / 2;

            return null;
        }

        public class Bounds
        {
            public Coordinate SouthWest { get; set; }
            public Coordinate NorthEast { get; set; }
        }

        public class Coordinate
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }

            public double X
            {
                get { return Longitude; }
                set { Longitude = value; }
            }

            public double Y
            {
                get { return Latitude; }
                set { Latitude = value; }
            }

            public Coordinate(double lat, double lng)
            {
                Latitude = lat;
                Longitude = lng;
            }

            public override string ToString()
            {
                return X.ToString() + ", " + Y.ToString();
            }
        }
    }

}
