using System;
using System.Drawing;
using System.Windows.Forms;
using AlgorithmVisualization.View.GLVisualization;
using AlgorithmVisualization.View.GLVisualization.GLUtil;
using MultiScaleTrajectories.Algorithm.Geometry;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace MultiScaleTrajectories.Trajectory
{
    abstract class TrajectoryCanvas : GLVisualization2D
    {
        private bool DraggingWorld;
        private Vector2d LastDraggingLocation;

        private Trajectory2D focusedTrajectory;


        protected TrajectoryCanvas()
        {
            MouseDown += HandleMouseDown;
            MouseUp += HandleMouseUp;
            MouseMove += HandleMouseMove;
            MouseWheel += HandleMouseWheel;

            WorldOriginChanged += () => focusedTrajectory = null;
            ZoomFactorChanged += () => focusedTrajectory = null;

            DraggingWorld = false;
        }

        protected abstract override void RenderWorld();

        protected override void OnResize(EventArgs e)
        {
            if (focusedTrajectory != null)
                LookAtTrajectory(focusedTrajectory);

            base.OnResize(e);
        }

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
                BoundingBox2D boundingBox = trajectory.BuildBoundingBox();
                LookAt(boundingBox.Center.X, boundingBox.Center.Y, 1.1 * boundingBox.Width, 1.1 * boundingBox.Height);
                focusedTrajectory = trajectory;
            }
            else
            {
                focusedTrajectory = null;
            }
        }

        protected void DrawPoint(Point2D point, double radius, int numSegments, Color color, int? name = null)
        {
            GL.PushMatrix();
            GL.Color3(color);

            GL.Translate(point.X, point.Y, 1f);

            if (name != null)
                GL.LoadName((int)name);

            GLUtil2D.DrawCircle(radius, numSegments);

            GL.PopMatrix();
        }

        protected void DrawTrajectoryEdges(Trajectory2D trajectory, float lineWidth, Color color)
        {
            GL.LineWidth(lineWidth);
            GL.Color3(color);
            GL.Begin(PrimitiveType.LineStrip);
            foreach (Point2D p in trajectory)
            {
                GL.Vertex3(p.X, p.Y, -1f);
            }
            GL.End();
        }

        protected void DrawTrajectoryPoints(Trajectory2D trajectory, double radius, int numSegments, Func<Point2D, Color> colorFunc, Func<Point2D, int> nameFunc = null)
        {
            foreach (Point2D p in trajectory)
            {
                DrawPoint(p, radius, numSegments, colorFunc(p), nameFunc(p));
            }
        }
    }
}
