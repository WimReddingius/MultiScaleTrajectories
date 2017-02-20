using MultiScaleTrajectories.Algorithm;
using MultiScaleTrajectories.Algorithm.SingleTrajectory;
using MultiScaleTrajectories.View;
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
    abstract class Visualization2D<I, O> : GLControl
    {

        protected PickNameManager PickManager;
        protected VisualizationMode Mode;

        protected I Input;
        protected O Output;

        public Visualization2D(I input) : base(new OpenTK.Graphics.GraphicsMode(32, 24, 0, 8))
        {
            CreateControl();

            Paint += new PaintEventHandler(this.Render);

            PickManager = new PickNameManager();

            VisualizeInput(input);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(0.7f, 0.7f, 0.7f, 0.0f);  // Background
            GL.Enable(EnableCap.DepthTest);         //Enable correct Z Drawings
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

        protected void Render(object sender, PaintEventArgs e)
        {
            GL.MatrixMode(MatrixMode.Modelview);

            Clear();
            RenderWorld();

            SwapBuffers();
        }

        protected abstract void RenderWorld();

        protected void Clear()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        protected int Pick(int x, int y)
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

        protected Vector2 getWorldCoordinates(int viewPortX, int viewPortY)
        {
            float worldX = viewPortX -(ClientRectangle.Width / 2);
            float worldY = viewPortY - (ClientRectangle.Height / 2);
            return new Vector2(worldX, worldY);
        }

        protected virtual void SwitchMode(VisualizationMode newMode)
        {
            Mode = newMode;
            Refresh();
        }

        protected virtual void SetInput(I Input)
        {
            this.Input = Input;
        }

        protected virtual void SetOutput(O Output)
        {
            this.Output = Output;
        }

        public void VisualizeInput(I Input)
        {
            SetInput(Input);
            SwitchMode(VisualizationMode.INPUT);
        }

        public void VisualizeOutput(O Output)
        {
            SetOutput(Output);
            SwitchMode(VisualizationMode.OUTPUT);
        }

    }
}
