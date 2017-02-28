﻿using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.Algorithm.SingleTrajectory;
using MultiScaleTrajectories.View.Visualization;
using MultiScaleTrajectories.View.Visualization.GL;
using OpenTK;
using OpenTK.Input;
using MultiScaleTrajectories.Controller.Util;

namespace MultiScaleTrajectories.View.SingleTrajectory.Input
{
    class STInputVisualization : GLVisualization2D, IDataLoader<STInput>
    {
        private Point2D LastSelectedPoint;
        private bool DraggingPoint;

        private STInput Input;

        public STInputVisualization()
        {
            MouseDown += HandleMouseDown;
            MouseUp += HandleMouseUp;
            MouseMove += HandleMouseMove;

            DraggingPoint = false;
        }

        protected override void RenderWorld()
        {
            RenderEdges();
            RenderPoints();
            RenderHud();
        }

        private void RenderPoints()
        {
            for (int i = 0; i < Input.Trajectory.Count; i++)
            {
                Point2D p = Input.Trajectory[i];
                Color color;
                if (p.Equals(LastSelectedPoint))
                    color = Color.Blue;
                else
                    color = Color.Red;

                OpenTK.Graphics.OpenGL.GL.LoadName(PickManager.GetPickingId(p));
                Util2D.DrawPoint(p, 3.5f, color);
            }
        }

        private void RenderEdges()
        {
            Util2D.DrawEdges(Input.Trajectory, 2.5f, Color.Red);
        }

        private void RenderHud()
        {
            int padding = 5;
            string text = "Editing";
            Color color = Color.Black;
            Util2D.RenderText((-ClientRectangle.Width / 2) + padding, (-ClientRectangle.Height / 2) + padding, text, color);
        }

        private void HandleMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (Mouse.GetState().IsButtonDown(MouseButton.Left) && DraggingPoint)
            {
                Vector2 worldCoord = GetWorldCoordinates(e.X, e.Y);
                LastSelectedPoint.SetPosition(worldCoord.X, worldCoord.Y);
            }
            Refresh();
        }

        private void HandleMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int pickId = Pick(e.X, e.Y);

            if (e.Button == MouseButtons.Left)
            {
                if (PickManager.PickingHit(pickId))
                { //clicked on point
                    LastSelectedPoint = (Point2D)PickManager.GetPickedObject(pickId);
                }
                else
                {  //clicked on empty space for new point

                    int index = Input.Trajectory.IndexOf(LastSelectedPoint);

                    if (!Input.Trajectory.Any())     //fresh trajectory
                        index = -1;
                    else if (index == -1)           //last selected point was removed
                        index = Input.Trajectory.Count - 1;

                    Vector2 worldCoord = GetWorldCoordinates(e.X, e.Y);
                    Point2D p = Input.Trajectory.InsertPoint(worldCoord.X, worldCoord.Y, index + 1);
                    PickManager.AssignPickId(p);

                    LastSelectedPoint = p;
                }

                DraggingPoint = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (PickManager.PickingHit(pickId))
                { //clicked on point
                    Point2D pointToBeRemoved = (Point2D)PickManager.GetPickedObject(pickId);
                    Input.Trajectory.Remove(pointToBeRemoved);
                }
            }
            Refresh();
        }

        private void HandleMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DraggingPoint = false;
            }
            Refresh();
        }

        public void LoadData(STInput input)
        {
            Input = input;
            Input.Replaced += ReloadInput;
            ReloadInput();
        }

        private void ReloadInput()
        {
            foreach (Point2D p in Input.Trajectory)
            {
                PickManager.AssignPickId(p);
            }

            Refresh();
        }

    }
}
