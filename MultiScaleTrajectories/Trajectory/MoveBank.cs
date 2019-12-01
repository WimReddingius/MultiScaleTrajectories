using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using MultiScaleTrajectories.AlgoUtil.Geometry;

namespace MultiScaleTrajectories.Trajectory
{
    static class MoveBank
    {
        public static Dictionary<string, Trajectory2D> ReadTrajectories(string fileName, bool findOnlyOne = false)
        {
            var trajectories = new Dictionary<string, Trajectory2D>();

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
                string lastTrajectoryId = null;

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    //end of stream reached
                    if (line == null)
                        break;

                    var trajectoryId = line.Split(',')[trajectoryIdCol].Replace("\"", "");
                    Debug.Assert(trajectoryId != null, "trajectoryId != null");

                    //new trajectory reached
                    if (trajectoryId != lastTrajectoryId)
                    {
                        trajectories[trajectoryId] = new Trajectory2D();

                        //first trajectory has been processed fully
                        if (findOnlyOne && lastTrajectoryId != null)
                            break;

                        lastTrajectoryId = trajectoryId;
                    }

                    var longitudeStr = line.Split(',')[longitudeCol].Replace(',', '.');
                    var latitudeStr = line.Split(',')[latitudeCol].Replace(',', '.');

                    //rare artifact where longitude and latitude is missing
                    if (longitudeStr == "" || latitudeStr == "")
                        continue;

                    var longitude = Convert.ToSingle(longitudeStr, CultureInfo.InvariantCulture);
                    var latitude = Convert.ToSingle(latitudeStr, CultureInfo.InvariantCulture);

                    //longitude as x, latitude as y
                    trajectories[trajectoryId].AppendPoint(longitude, latitude);
                }
            }

            return trajectories;
        }
    }
}
