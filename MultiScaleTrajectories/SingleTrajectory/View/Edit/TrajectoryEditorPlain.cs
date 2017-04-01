using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AlgorithmVisualization.Controller.Edit;
using AlgorithmVisualization.View.GLVisualization.GLUtil;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;
using MultiScaleTrajectories.View;
using OpenTK;
using OpenTK.Input;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace MultiScaleTrajectories.SingleTrajectory.View.Edit
{
    class TrajectoryEditorPlain : TrajectoryGLVisualization, IInputEditor<STInput>
    {
        private Point2D lastSelectedPoint;
        private bool draggingPoint;
        private STInput input;


        public TrajectoryEditorPlain()
        {
            MouseDown += HandleMouseDown;
            MouseUp += HandleMouseUp;
            MouseMove += HandleMouseMove;

            Visible = false;
            draggingPoint = false;

            Name = "No Map";
        }

        protected override void RenderWorld()
        {
            Func<Point2D, Color> colorFunc = (p) =>
            {
                if (p.Equals(lastSelectedPoint))
                    return Color.Blue;

                return Color.Red;
            };

            DrawTrajectoryEdges(input.Trajectory, 2.5f, Color.Red);
            DrawTrajectoryPoints(input.Trajectory, 4.5 / ZoomFactor, 6, colorFunc, (p) => PickManager.GetPickingId(p));
        }

        protected override void RenderHud()
        {
            int padding = 5;
            string text = "Editing " + input.Name;
            Color color = Color.Black;
            GLUtil2D.RenderText(padding, padding, text, color);
        }

        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.GetState().IsButtonDown(MouseButton.Left) && draggingPoint)
            {
                Vector2d worldCoord = GetWorldCoordinates(e.X, e.Y);
                lastSelectedPoint.SetPosition(worldCoord.X, worldCoord.Y);
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
                    lastSelectedPoint = (Point2D)PickManager.GetPickedObject(pickId);
                }
                else
                {  //clicked on empty space for new point

                    int index = input.Trajectory.IndexOf(lastSelectedPoint);

                    if (!input.Trajectory.Any())     //fresh trajectory
                        index = -1;
                    else if (index == -1)           //last selected point was removed
                        index = input.Trajectory.Count - 1;

                    Vector2d worldCoord = GetWorldCoordinates(e.X, e.Y);
                    Point2D p = input.Trajectory.InsertPoint(worldCoord.X, worldCoord.Y, index + 1);
                    PickManager.AssignPickId(p);

                    lastSelectedPoint = p;
                }

                draggingPoint = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (PickManager.PickingHit(pickId))
                {
                    //clicked on point
                    Point2D pointToBeRemoved = (Point2D) PickManager.GetPickedObject(pickId);
                    input.Trajectory.Remove(pointToBeRemoved);
                }
            }
            Refresh();
        }

        private void HandleMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                draggingPoint = false;
            }
            Refresh();
        }

        public void LoadInput(STInput input)
        {
            Visible = false;

            this.input = input;
            LookAtTrajectory(this.input.Trajectory);

            foreach (Point2D p in this.input.Trajectory)
            {
                PickManager.AssignPickId(p);
            }

            Visible = true;
        }

    }
}
