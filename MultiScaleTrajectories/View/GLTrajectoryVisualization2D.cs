using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Windows.Forms;
using AlgorithmVisualization.View.GLVisualization;
using Google.Maps;
using Google.Maps.StaticMaps;
using MultiScaleTrajectories.Algorithm.Geometry;
using OpenTK;
using OpenTK.Input;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace MultiScaleTrajectories.View
{
    abstract class GLTrajectoryVisualization2D : GLVisualization2D
    {
        private bool DraggingWorld;
        private Vector2d LastDraggingLocation;


        protected GLTrajectoryVisualization2D()
        {
            MouseDown += HandleMouseDown;
            MouseUp += HandleMouseUp;
            MouseMove += HandleMouseMove;
            MouseWheel += HandleMouseWheel;

            DraggingWorld = false;
        }

        protected abstract override void RenderWorld();

        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.GetState().IsButtonDown(MouseButton.Right) && DraggingWorld)
            {
                Vector2d mouseLoc = GetWorldCoordinates(e.Location.X, e.Location.Y);
                Vector2d displacement = Vector2d.Subtract(LastDraggingLocation, mouseLoc);

                WorldOrigin = Vector2d.Add(WorldOrigin, displacement);

                Vector2d newMouseLoc = GetWorldCoordinates(e.Location.X, e.Location.Y);
                LastDraggingLocation = newMouseLoc;
            }
            Refresh();
        }

        private void HandleMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DraggingWorld = true;
                LastDraggingLocation = GetWorldCoordinates(e.Location.X, e.Location.Y);
            }
        }

        private void HandleMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DraggingWorld = false;
            }
        }

        private void HandleMouseWheel(object sender, MouseEventArgs e)
        {
            double scaling;
            double zoomOutScaling = 0.9f;

            if (e.Delta < 0)
                scaling = zoomOutScaling;
            else if (e.Delta > 0)
                scaling = 1 / zoomOutScaling;
            else
                return;

            ZoomFactor *= scaling;
            Refresh();
        }

        protected void LookAtTrajectory(Trajectory2D trajectory)
        {
            if (trajectory.Count > 0)
            {
                BoundingBox2D boundingBox = trajectory.GetBoundingBox();
                LookAt(boundingBox.Center.X, boundingBox.Center.Y, 1.1 * boundingBox.Width, 1.1 * boundingBox.Height);

                //var map = new StaticMapRequest
                //{
                //    Center = new LatLng(boundingBox.Center.Y, boundingBox.Center.X),
                //    Size = new Size(ClientRectangle.Width, ClientRectangle.Height),
                //    Zoom = 10,
                //    Sensor = false
                //};
                //SaveImage(@"files\map.png", map.ToUri(), ImageFormat.Png);
            }
        }

        private void SaveImage(string filename, Uri uri, ImageFormat format)
        {
            var client = new WebClient();
            var stream = client.OpenRead(uri);
            var bitmap = new Bitmap(stream);

            bitmap.Save(filename, format);

            stream.Flush();
            stream.Close();
            client.Dispose();
        }

    }
}
