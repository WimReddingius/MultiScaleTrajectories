using MultiScaleTrajectories.algorithm;
using MultiScaleTrajectories.algorithm.SingleTrajectory;
using MultiScaleTrajectories.view;
using MultiScaleTrajectories.View.Util;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MultiScaleTrajectories
{
    class Visualizer : GLControl
    {

        private PickNameManager PickManager;
        private VisualizationMode Mode;
        private Point2D LastSelectedPoint;
        private bool DraggingPoint;

        public Trajectory2D InputTrajectory;
        public List<double> InputEpsilons;

        public STAlgorithm CurrentAlgorithm;
        public STSolution CurrentSolution;
        public int CurrentLevel;


        public Visualizer() : base(new OpenTK.Graphics.GraphicsMode(32, 24, 0, 8))
        {
            CreateControl();

            Paint += new PaintEventHandler(this.Render);
            MouseDown += new MouseEventHandler(this.HandleMouseDown);
            MouseUp += new MouseEventHandler(this.HandleMouseUp);
            MouseMove += new MouseEventHandler(this.HandleMouseMove);
            MouseWheel += new MouseEventHandler(this.HandleMouseWheel);

            InputEpsilons = new List<double>();
            InputTrajectory = new Trajectory2D();

            PickManager = new PickNameManager();
            DraggingPoint = false;
            SwitchMode(VisualizationMode.EDIT);
        }

        internal void visualizeSolution()
        {
            CurrentSolution = CurrentAlgorithm.Solve(InputTrajectory, InputEpsilons);
            SwitchMode(VisualizationMode.SOLUTION);
            Refresh();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(0.7f, 0.7f, 0.7f, 0.0f);  // Background
            GL.Enable(EnableCap.DepthTest);       //Enable correct Z Drawings
            GL.DepthFunc(DepthFunction.Always);     //Enable correct Z Drawings

            SetGLPerspective();

            GLTextRenderer.GenerateFontImage();
            GLTextRenderer.LoadTexture();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);            
            SetGLPerspective();
        }

        private void SetGLPerspective()
        {
            GL.Viewport(0, 0, ClientRectangle.Width, ClientRectangle.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, ClientRectangle.Width, ClientRectangle.Height, 0, 5, -5);
            GL.Translate(ClientRectangle.Width / 2, ClientRectangle.Height / 2, 0.0);
        }

        private void Render(object sender, PaintEventArgs e)
        {
            GL.MatrixMode(MatrixMode.Modelview);
            Clear();

            renderEdges();
            RenderPoints();
            RenderHUD();

            //TODO: draw up to date epsilons if in edit mode

            SwapBuffers();
        }

        private void RenderPoints() 
        {
            Trajectory2D trajectory = getRenderedTrajectory();

            for (int i = 0; i < trajectory.Count; i++)
            {
                Point2D p = trajectory[i];
                GL.LoadName(PickManager.getPickingId(p));
                GL.PushMatrix();

                if (p.Equals(LastSelectedPoint) && Mode == VisualizationMode.EDIT)
                    GL.Color3(Color.Blue);
                else
                    GL.Color3(Color.Red);

                GL.Translate(p.X, p.Y, 1f);
                GLU.drawCircle(5.0f);
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
                case VisualizationMode.EDIT:
                    str1 = "Editing";
                    break;
                case VisualizationMode.SOLUTION:
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

        private void Clear()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        //produces the picking id of the clicked object
        public int Pick(int x, int y)
        {

            // The selection buffer
            int[] buffer = new int[256];

            // Get the viewport info
            int[] viewport = new int[4];
            GL.GetInteger(GetPName.Viewport, viewport);

            float pickRegionWidth = 1f;
            float pickRegionHeight = 1f;

            // The number of "hits" (objects within the pick area).
            int hits;

            // Set the buffer that OpenGL uses for selection to our buffer
            GL.SelectBuffer(256, buffer);

            // Change to selection mode
            GL.RenderMode(RenderingMode.Select);

            // Initialize the name stack (used for identifying which object was selected)
            GL.InitNames();
            GL.PushName(-1);

            GL.MatrixMode(MatrixMode.Projection);
            GL.PushMatrix();

            /*  create pickRegionWidth x pickRegionHeight pixel picking region near cursor location */
            SetGLPerspective();
            GL.Scale(viewport[2] / pickRegionWidth, viewport[3] / pickRegionHeight, 1.0f);
            GL.Translate((viewport[2] / 2) - x, (viewport[3] / 2) - y, 0f);

            Render(null, null);

            // reset OpenGL state
            GL.MatrixMode(MatrixMode.Projection);
            GL.PopMatrix();
            GL.Flush();

            // Exit selection mode and return to render mode, returns number selected
            hits = GL.RenderMode(RenderingMode.Render);

            int picked = -1;

            // Objects were drawn where the mouse was
            int depth = int.MaxValue;
            if (hits > 0)
            {
                for (int i = 0; i < hits; i++)
                {
                    int hitId = buffer[i * 4 + 3];
                    int hitDepth = buffer[i * 4 + 1];

                    // Loop through all the detected hits
                    // If this object is closer to us than the one we have selected, and it has an assigned hit name/id
                    if (hitDepth < depth && hitId != -1)
                    {
                        picked = hitId;     // Select The Closer Object
                        depth = hitDepth;   // Store How Far Away It Is
                    }
                }
            }

            return picked;
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

            if (Mode == VisualizationMode.EDIT)
            {
                int pickId = Pick(e.X, e.Y);

                if (e.Button == MouseButtons.Left)
                {
                    if (PickManager.pickingHit(pickId))
                    { //clicked on point
                        LastSelectedPoint = (Point2D)PickManager.getPickedObject(pickId);
                    }
                    else {  //clicked on empty space for new point

                        int index = InputTrajectory.IndexOf(LastSelectedPoint);

                        if (!InputTrajectory.Any())     //fresh trajectory
                            index = -1;
                        else if (index == -1)           //last selected point was removed
                            index = InputTrajectory.Count() - 1;

                        Vector2 worldCoord = getWorldCoordinates(e.X, e.Y);
                        Point2D p = InputTrajectory.InsertPoint(worldCoord.X, worldCoord.Y, index + 1);
                        PickManager.AssignPickId(p);
                        LastSelectedPoint = p;
                    }

                    DraggingPoint = true;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    if (PickManager.pickingHit(pickId))
                    { //clicked on point
                        Point2D pointToBeRemoved = (Point2D)PickManager.getPickedObject(pickId);
                        InputTrajectory.Remove(pointToBeRemoved);
                    }
                }
            }
            Refresh();
        }

        private void HandleMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (Mode == VisualizationMode.EDIT)
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
            if (Mode == VisualizationMode.SOLUTION)
            {

                bool scrollUp = e.Delta > 0;
                bool scrollDown = e.Delta < 0;

                if (scrollUp && CurrentLevel < InputEpsilons.Count)
                {  // here up
                    CurrentLevel++;
                }

                if (scrollDown && CurrentLevel > 0)
                {  // here down
                    CurrentLevel--;
                }
            }
            Refresh();
        }

        private Vector2 getWorldCoordinates(int viewPortX, int viewPortY)
        {
            float worldX = viewPortX -(ClientRectangle.Width / 2);
            float worldY = viewPortY - (ClientRectangle.Height / 2);
            return new Vector2(worldX, worldY);
        }

        private Trajectory2D getRenderedTrajectory()
        {
            Trajectory2D trajectory;
            if (CurrentLevel == 0 || Mode == VisualizationMode.EDIT)
            {
                trajectory = InputTrajectory;
            }
            else {
                trajectory = CurrentSolution.getTrajectoryAtLevel(CurrentLevel);
            }

            return trajectory;
        }

        internal void SwitchMode(VisualizationMode newMode)
        {
            Mode = newMode;
            CurrentLevel = 0;
            Refresh();
        }
        
    }
}
