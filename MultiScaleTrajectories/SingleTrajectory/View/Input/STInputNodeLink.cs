using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AlgorithmVisualization.Controller;
using AlgorithmVisualization.Controller.Edit;
using AlgorithmVisualization.View.Visualization.GLUtil;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;
using MultiScaleTrajectories.View;
using OpenTK;
using OpenTK.Input;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace MultiScaleTrajectories.SingleTrajectory.View.Input
{
    class STInputNodeLink : GLTrajectoryVisualization2D, IInputLoader<STInput>
    {
        private Point2D LastSelectedPoint;
        private bool DraggingPoint;

        private STInput Input;

        public STInputNodeLink()
        {
            MouseDown += HandleMouseDown;
            MouseUp += HandleMouseUp;
            MouseMove += HandleMouseMove;

            DraggingPoint = false;
        }

        protected override void RenderWorld()
        {
            Func<Point2D, Color> colorFunc = (p) =>
            {
                if (p.Equals(LastSelectedPoint))
                    return Color.Blue;

                return Color.Red;
            };

            GLUtilTrajectory2D.DrawEdges(Input.Trajectory, 2.5f, Color.Red);
            GLUtilTrajectory2D.DrawPoints(Input.Trajectory, 3.5 / ZoomFactor, 6, colorFunc, (p) => PickManager.GetPickingId(p));
        }

        protected override void RenderHud()
        {
            int padding = 5;
            string text = "Editing";
            Color color = Color.Black;
            GLUtil2D.RenderText((-ClientRectangle.Width / 2) + padding, (-ClientRectangle.Height / 2) + padding, text, color);
        }

        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.GetState().IsButtonDown(MouseButton.Left) && DraggingPoint)
            {
                Vector2d worldCoord = GetWorldCoordinates(e.X, e.Y);
                LastSelectedPoint.SetPosition(worldCoord.X, worldCoord.Y);
            }

            Refresh();
        }

        private void HandleMouseDown(object sender, MouseEventArgs e)
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

                    Vector2d worldCoord = GetWorldCoordinates(e.X, e.Y);
                    Point2D p = Input.Trajectory.InsertPoint(worldCoord.X, worldCoord.Y, index + 1);
                    PickManager.AssignPickId(p);

                    LastSelectedPoint = p;
                }

                DraggingPoint = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (PickManager.PickingHit(pickId))
                {
                    //clicked on point
                    Point2D pointToBeRemoved = (Point2D) PickManager.GetPickedObject(pickId);
                    Input.Trajectory.Remove(pointToBeRemoved);
                }
            }
            Refresh();
        }

        private void HandleMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DraggingPoint = false;
            }
            Refresh();
        }

        public void LoadInput(STInput input)
        {
            Input = input;
            LookAtTrajectory(Input.Trajectory);

            foreach (Point2D p in Input.Trajectory)
            {
                PickManager.AssignPickId(p);
            }

            Refresh();
        }

    }
}
