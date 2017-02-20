using MultiScaleTrajectories.Algorithm;
using MultiScaleTrajectories.Algorithm.SingleTrajectory;
using MultiScaleTrajectories.View;
using MultiScaleTrajectories.View.Util;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MultiScaleTrajectories.View.SingleTrajectory
{
    class STVisualization : Visualization2D<STInput, STOutput>
    {

        private Point2D LastSelectedPoint;
        private bool DraggingPoint;
        public int CurrentLevel;

        public STVisualization(STInput input) : base(input)
        {
            MouseDown += new MouseEventHandler(this.HandleMouseDown);
            MouseUp += new MouseEventHandler(this.HandleMouseUp);
            MouseMove += new MouseEventHandler(this.HandleMouseMove);
            MouseWheel += new MouseEventHandler(this.HandleMouseWheel);

            DraggingPoint = false;
        }      

        protected override void RenderWorld()
        {
            renderEdges();
            RenderPoints();
            RenderHUD();
        }

        private void RenderPoints()
        {
            Trajectory2D trajectory = getRenderedTrajectory();

            for (int i = 0; i < trajectory.Count; i++)
            {
                Point2D p = trajectory[i];

                if (Mode == VisualizationMode.INPUT)
                    GL.LoadName(PickManager.getPickingId(p));

                GL.PushMatrix();

                if (p.Equals(LastSelectedPoint) && Mode == VisualizationMode.INPUT)
                    GL.Color3(Color.Blue);
                else
                    GL.Color3(Color.Red);

                GL.Translate(p.X, p.Y, 1f);
                GLUtil.drawCircle(5.0f);
                GL.PopMatrix();
            }
        }

        private void renderEdges()
        {
            Trajectory2D trajectory = getRenderedTrajectory();

            GL.LineWidth(2.5f);
            GL.Color3(Color.Red);
            GL.Begin(PrimitiveType.LineStrip);
            foreach (Point2D p in trajectory)
            {
                GL.Vertex3(p.X, p.Y, -1f);
            }
            GL.End();
        }

        private void RenderHUD()
        {
            String str1 = "";
            String str2 = "";
            switch (Mode)
            {
                case VisualizationMode.INPUT:
                    str1 = "Editing";
                    break;
                case VisualizationMode.OUTPUT:
                    str1 = "Solution";
                    str2 = "Level " + CurrentLevel;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            int padding = 5;

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Color3(Color.Black);
            GLTextRenderer.DrawText((-ClientRectangle.Width / 2) + padding, (-ClientRectangle.Height / 2) + padding, str1);
            GLTextRenderer.DrawText((-ClientRectangle.Width / 2) + padding, (-ClientRectangle.Height / 2) + padding + GLTextRenderer.FontSize + 5, str2);
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.Texture2D);

        }

        private void HandleMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (Mouse.GetState().IsButtonDown(MouseButton.Left) && DraggingPoint)
            {
                Vector2 worldCoord = getWorldCoordinates(e.X, e.Y);
                LastSelectedPoint.setPosition(worldCoord.X, worldCoord.Y);
            }
            Refresh();
        }

        private void HandleMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            if (Mode == VisualizationMode.INPUT)
            {
                int pickId = Pick(e.X, e.Y);

                if (e.Button == MouseButtons.Left)
                {
                    if (PickManager.pickingHit(pickId))
                    { //clicked on point
                        LastSelectedPoint = (Point2D)PickManager.getPickedObject(pickId);
                    }
                    else
                    {  //clicked on empty space for new point

                        int index = Input.Trajectory.IndexOf(LastSelectedPoint);

                        if (!Input.Trajectory.Any())     //fresh trajectory
                            index = -1;
                        else if (index == -1)           //last selected point was removed
                            index = Input.Trajectory.Count() - 1;

                        Vector2 worldCoord = getWorldCoordinates(e.X, e.Y);
                        Point2D p = InsertPoint(worldCoord.X, worldCoord.Y, index + 1);
                        LastSelectedPoint = p;
                    }

                    DraggingPoint = true;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    if (PickManager.pickingHit(pickId))
                    { //clicked on point
                        Point2D pointToBeRemoved = (Point2D)PickManager.getPickedObject(pickId);
                        Input.Trajectory.Remove(pointToBeRemoved);
                    }
                }
            }
            Refresh();
        }

        private void HandleMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (Mode == VisualizationMode.INPUT)
            {
                if (e.Button == MouseButtons.Left)
                {
                    DraggingPoint = false;
                }
            }
            Refresh();
        }

        private void HandleMouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (Mode == VisualizationMode.OUTPUT)
            {

                bool scrollUp = e.Delta > 0;
                bool scrollDown = e.Delta < 0;

                if (scrollUp && CurrentLevel > 0)
                {  // here up
                    CurrentLevel--;
                }

                if (scrollDown && CurrentLevel < Input.NumLevels)
                {  // here down
                    CurrentLevel++;
                }
            }
            Refresh();
        }

        private Trajectory2D getRenderedTrajectory()
        {
            Trajectory2D trajectory;
            if (CurrentLevel == 0 || Mode == VisualizationMode.INPUT)
            {
                trajectory = Input.Trajectory;
            }
            else
            {
                trajectory = Output.getTrajectoryAtLevel(CurrentLevel);
            }

            return trajectory;
        }

        private Point2D InsertPoint(float x, float y, int index)
        {
            Point2D p = Input.Trajectory.InsertPoint(x, y, index);
            PickManager.AssignPickId(p);
            return p;
        }

        private Point2D AppendPoint(float x, float y)
        {
            return InsertPoint(x, y, Input.Trajectory.Count);
        }

        protected override void SwitchMode(VisualizationMode newMode)
        {
            base.SwitchMode(newMode);

            if (newMode == VisualizationMode.OUTPUT)
                CurrentLevel = Input.NumLevels;
        }

        protected override void SetInput(STInput Input)
        {
            base.SetInput(Input);

            foreach (Point2D p in Input.Trajectory)
            {
                PickManager.AssignPickId(p);
            }
        }

        protected override void SetOutput(STOutput Output)
        {
            base.SetOutput(Output);
        }

    }
}
