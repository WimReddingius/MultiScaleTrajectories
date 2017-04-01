using System.Linq;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using MultiScaleTrajectories.Algorithm.Geometry;

namespace MultiScaleTrajectories.View
{
    partial class TrajectoryGMap : UserControl
    {
        private readonly GMapOverlay routesOverlay;
        public GMapControl MapControl => gMap;

        public TrajectoryGMap()
        {
            InitializeComponent();

            Name = "Map";

            GMaps.Instance.Mode = AccessMode.ServerOnly;

            gMap.MapProvider = GoogleMapProvider.Instance;
            gMap.ShowCenter = false;
            gMap.DragButton = MouseButtons.Right;

            routesOverlay = new GMapOverlay("routes");
            gMap.Overlays.Add(routesOverlay);
        }

        public void LookAtTrajectory(Trajectory2D trajectory)
        {
            var bb = trajectory.BuildBoundingBox();
            gMap.SetZoomToFitRect(new RectLatLng(bb.MaxY, bb.MinX, bb.Width, bb.Height));
        }

        public void DrawTrajectory(Trajectory2D trajectory)
        {
            var points = trajectory.Select(point => new PointLatLng(point.Y, point.X)).ToList();
            var route = new GMapRoute(points, "points")
            {
                Stroke = { Width = 2 }
            };

            routesOverlay.Routes.Add(route);
        }

        public void DrawSingleTrajectory(Trajectory2D trajectory)
        {
            ClearTrajectories();
            DrawTrajectory(trajectory);
        }

        public void ClearTrajectories()
        {
            routesOverlay.Routes.Clear();
        }

    }
}
