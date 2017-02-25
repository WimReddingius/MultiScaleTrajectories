using MultiScaleTrajectories.Algorithm;
using MultiScaleTrajectories.Algorithm.SingleTrajectory;
using MultiScaleTrajectories.View.Type.Visualization.GL;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MultiScaleTrajectories.View.Type.Visualization.SingleTrajectory
{
    class STVisualization : GLVisualization2D<STInput, STOutput>
    {

        private Point2D LastSelectedPoint;
        private bool DraggingPoint;
        public int CurrentLevel;

        public STVisualization(AlgorithmRunner<STInput, STOutput> runner) : base(runner)
        {
            MouseDown += new MouseEventHandler(this.HandleMouseDown);
            MouseUp += new MouseEventHandler(this.HandleMouseUp);
            MouseMove += new MouseEventHandler(this.HandleMouseMove);
            KeyDown += new KeyEventHandler(this.HandleArrowKeys);

            AlgorithmRunner.Input.Loaded += () => { InitializeNewInput(); Refresh(); };

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
                    OpenTK.Graphics.OpenGL.GL.LoadName(PickManager.getPickingId(p));

                OpenTK.Graphics.OpenGL.GL.PushMatrix();

                if (p.Equals(LastSelectedPoint) && Mode == VisualizationMode.INPUT)
                    OpenTK.Graphics.OpenGL.GL.Color3(Color.Blue);
                else
                    OpenTK.Graphics.OpenGL.GL.Color3(Color.Red);

                OpenTK.Graphics.OpenGL.GL.Translate(p.X, p.Y, 1f);
                Util.drawCircle(3.5f);
                OpenTK.Graphics.OpenGL.GL.PopMatrix();
            }
        }

        private void renderEdges()
        {
            Trajectory2D trajectory = getRenderedTrajectory();

            OpenTK.Graphics.OpenGL.GL.LineWidth(2.5f);
            OpenTK.Graphics.OpenGL.GL.Color3(Color.Red);
            OpenTK.Graphics.OpenGL.GL.Begin(PrimitiveType.LineStrip);
            foreach (Point2D p in trajectory)
            {
                OpenTK.Graphics.OpenGL.GL.Vertex3(p.X, p.Y, -1f);
            }
            OpenTK.Graphics.OpenGL.GL.End();
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

            OpenTK.Graphics.OpenGL.GL.Enable(EnableCap.Texture2D);
            OpenTK.Graphics.OpenGL.GL.Enable(EnableCap.Blend);
            OpenTK.Graphics.OpenGL.GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            OpenTK.Graphics.OpenGL.GL.Color3(Color.Black);
            GL.TextRenderer.DrawText((-ClientRectangle.Width / 2) + padding, (-ClientRectangle.Height / 2) + padding, str1);
            GL.TextRenderer.DrawText((-ClientRectangle.Width / 2) + padding, (-ClientRectangle.Height / 2) + padding + GL.TextRenderer.FontSize + 5, str2);
            OpenTK.Graphics.OpenGL.GL.Disable(EnableCap.Blend);
            OpenTK.Graphics.OpenGL.GL.Disable(EnableCap.Texture2D);

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

                        int index = AlgorithmRunner.Input.Trajectory.IndexOf(LastSelectedPoint);

                        if (!AlgorithmRunner.Input.Trajectory.Any())     //fresh trajectory
                            index = -1;
                        else if (index == -1)           //last selected point was removed
                            index = AlgorithmRunner.Input.Trajectory.Count() - 1;

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
                        AlgorithmRunner.Input.Trajectory.Remove(pointToBeRemoved);
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

        private void HandleArrowKeys(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (Mode == VisualizationMode.OUTPUT)
            {

                bool levelDown = e.KeyCode == Keys.Up;
                bool levelUp = e.KeyCode == Keys.Down;

                if (levelDown && CurrentLevel > 0)
                {  // here up
                    CurrentLevel--;
                }

                if (levelUp && CurrentLevel < AlgorithmRunner.Input.NumLevels)
                {  // here down
                    CurrentLevel++;
                }
            }
            Refresh();
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        private Trajectory2D getRenderedTrajectory()
        {
            Trajectory2D trajectory;
            if (CurrentLevel == 0 || Mode == VisualizationMode.INPUT)
            {
                trajectory = AlgorithmRunner.Input.Trajectory;
            }
            else
            {
                trajectory = AlgorithmRunner.Output.getTrajectoryAtLevel(CurrentLevel);
            }

            return trajectory;
        }

        private Point2D InsertPoint(float x, float y, int index)
        {
            Point2D p = AlgorithmRunner.Input.Trajectory.InsertPoint(x, y, index);
            PickManager.AssignPickId(p);
            return p;
        }

        private Point2D AppendPoint(float x, float y)
        {
            return InsertPoint(x, y, AlgorithmRunner.Input.Trajectory.Count);
        }

        protected void InitializeNewInput()
        {
            foreach (Point2D p in AlgorithmRunner.Input.Trajectory)
            {
                PickManager.AssignPickId(p);
            }
        }

        protected override void SwitchMode(VisualizationMode mode)
        {
            switch (mode)
            {
                case VisualizationMode.INPUT:
                    break;
                case VisualizationMode.OUTPUT:
                    CurrentLevel = AlgorithmRunner.Output.NumLevels;
                    break;
            }

            base.SwitchMode(mode);
        }

    }
}
