using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using MultiScaleTrajectories.AlgoUtil.Geometry;

namespace MultiScaleTrajectories.Trajectory.View
{
    partial class TrajectoryGeo : UserControl
    {
        private readonly GMapOverlay routesOverlay;
        public GMapControl MapControl => gMap;

        public double LonPerPixel => MapControl.ViewArea.WidthLng / Width;

        public TrajectoryGeo()
        {
            InitializeComponent();

            Name = "Map";

            routesOverlay = new GMapOverlay("routes");
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gMap.MapProvider = GoogleMapProvider.Instance;
            gMap.ShowCenter = false;
            gMap.DragButton = MouseButtons.Right;
            gMap.Overlays.Add(routesOverlay);
            gMap.Paint += DrawScale;
        }

        public void LookAtTrajectory(Trajectory2D trajectory)
        {
            if (trajectory.Count > 0)
            {
                var bb = trajectory.BuildBoundingBox();
                gMap.SetZoomToFitRect(new RectLatLng(bb.MaxY, bb.MinX, bb.Width, bb.Height));
            }
        }

        public void DrawTrajectory(Trajectory2D trajectory)
        {
            var points = trajectory.Select(point => new PointLatLng(point.Y, point.X)).ToList();
            var route = new GMapRoute(points, "points")
            {
                Stroke = { Color = Color.Black, Width = 2 }
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

        protected void DrawScale(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var brush = new SolidBrush(Color.Blue);
            var pen = new Pen(brush);

            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var lonPer100Pixels = 100 * LonPerPixel;
            var font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);
            var str = string.Format(CultureInfo.InvariantCulture, "{0:N4}", lonPer100Pixels);

            g.DrawString(str, font, brush, ClientRectangle.Width - 95, ClientRectangle.Height - 25);
            g.DrawLine(pen, ClientRectangle.Width - 110, ClientRectangle.Height - 5, ClientRectangle.Width - 10, ClientRectangle.Height - 5);
            g.DrawLine(pen, ClientRectangle.Width - 110, ClientRectangle.Height - 5, ClientRectangle.Width - 110, ClientRectangle.Height - 10);
            g.DrawLine(pen, ClientRectangle.Width - 10, ClientRectangle.Height - 5, ClientRectangle.Width - 10, ClientRectangle.Height - 10);
        }

    }
}
