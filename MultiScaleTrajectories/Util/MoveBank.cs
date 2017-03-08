using System;
using System.Globalization;
using System.IO;
using System.Linq;
using MultiScaleTrajectories.Algorithm.Geometry;

namespace MultiScaleTrajectories.Util
{
    class MoveBank
    {
        public static bool IsMoveBankFile(string fileName)
        {
            var firstWord = "";
            using (StreamReader reader = new StreamReader(fileName))
            {
                var firstLine = reader.ReadLine();
                if (firstLine != null) {
                    firstWord = firstLine.Split(',').First();
                }
            }

            return firstWord == "event-id";
        }

        public static Trajectory2D ReadSingleTrajectory(string fileName)
        {
            Trajectory2D trajectory = new Trajectory2D();
            using (StreamReader reader = new StreamReader(fileName))
            {
                //pass past column headers
                var firstLine = reader.ReadLine();

                if (firstLine == null)
                    return null;

                var columns = firstLine.Split(',');
                var trajectoryIdCol = columns.ToList().FindIndex(str => str == "individual-local-identifier");
                var longitudeCol = columns.ToList().FindIndex(str => str == "location-long");
                var latitudeCol = columns.ToList().FindIndex(str => str == "location-lat");

                //correct columns not foud
                if (trajectoryIdCol == -1 || longitudeCol == -1 || latitudeCol == -1)
                    return null;

                //id of first trajectory in the data
                string trajectoryId = null;

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var newPointTrajectoryId = line.Split(',')[trajectoryIdCol];

                    //first point: initialize trajectory id
                    if (trajectoryId == null)
                        trajectoryId = newPointTrajectoryId;

                    //new trajectory reached
                    if (newPointTrajectoryId != trajectoryId)
                        break;

                    var longitudeStr = line.Split(',')[longitudeCol].Replace(',', '.');
                    var latitudeStr = line.Split(',')[latitudeCol].Replace(',', '.');

                    //rare artifact where longitude and latitude is missing
                    if (longitudeStr == "" || latitudeStr == "")
                        continue;

                    var longitude = Convert.ToSingle(longitudeStr, CultureInfo.InvariantCulture);
                    var latitude = Convert.ToSingle(latitudeStr, CultureInfo.InvariantCulture);

                    //longitude as x, latitude as y
                    trajectory.AppendPoint(longitude, latitude);
                }
            }
            return trajectory;
        }


    }
}
