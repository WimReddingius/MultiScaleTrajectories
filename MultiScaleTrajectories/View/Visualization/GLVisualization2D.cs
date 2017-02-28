using System;
using System.Windows.Forms;
using MultiScaleTrajectories.View.Visualization.GL;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace MultiScaleTrajectories.View.Visualization
{
    abstract class GLVisualization2D : GLVisualization
    {

        protected PickNameManager PickManager;

        protected GLVisualization2D()
        {
            PickManager = new PickNameManager();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            OpenTK.Graphics.OpenGL.GL.ClearColor(0.7f, 0.7f, 0.7f, 0.0f);  // Background
            OpenTK.Graphics.OpenGL.GL.Enable(EnableCap.DepthTest);         //Enable correct Z Drawings
            OpenTK.Graphics.OpenGL.GL.DepthFunc(DepthFunction.Always);     //Enable correct Z Drawings

            SetGLPerspective();

            GL.TextRenderer2D.GenerateFontImage();
            GL.TextRenderer2D.LoadTexture();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetGLPerspective();
        }

        private void SetGLPerspective()
        {
            OpenTK.Graphics.OpenGL.GL.Viewport(0, 0, ClientRectangle.Width, ClientRectangle.Height);
            OpenTK.Graphics.OpenGL.GL.MatrixMode(MatrixMode.Projection);
            OpenTK.Graphics.OpenGL.GL.LoadIdentity();
            OpenTK.Graphics.OpenGL.GL.Ortho(0, ClientRectangle.Width, ClientRectangle.Height, 0, 5, -5);
            OpenTK.Graphics.OpenGL.GL.Translate(ClientRectangle.Width / 2, ClientRectangle.Height / 2, 0.0);
        }

        protected override void Render(object sender, PaintEventArgs e)
        {
            OpenTK.Graphics.OpenGL.GL.MatrixMode(MatrixMode.Modelview);

            Clear();
            RenderWorld();

            SwapBuffers();
        }

        protected abstract void RenderWorld();

        protected void Clear()
        {
            OpenTK.Graphics.OpenGL.GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        protected int Pick(int x, int y)
        {

            // The selection buffer
            int[] buffer = new int[256];

            // Get the viewport info
            int[] viewport = new int[4];
            OpenTK.Graphics.OpenGL.GL.GetInteger(GetPName.Viewport, viewport);

            float pickRegionWidth = 1f;
            float pickRegionHeight = 1f;

            // The number of "hits" (objects within the pick area).
            int hits;

            // Set the buffer that OpenGL uses for selection to our buffer
            OpenTK.Graphics.OpenGL.GL.SelectBuffer(256, buffer);

            // Change to selection mode
            OpenTK.Graphics.OpenGL.GL.RenderMode(RenderingMode.Select);

            // Initialize the name stack (used for identifying which object was selected)
            OpenTK.Graphics.OpenGL.GL.InitNames();
            OpenTK.Graphics.OpenGL.GL.PushName(-1);

            OpenTK.Graphics.OpenGL.GL.MatrixMode(MatrixMode.Projection);
            OpenTK.Graphics.OpenGL.GL.PushMatrix();

            /*  create pickRegionWidth x pickRegionHeight pixel picking region near cursor location */
            SetGLPerspective();
            OpenTK.Graphics.OpenGL.GL.Scale(viewport[2] / pickRegionWidth, viewport[3] / pickRegionHeight, 1.0f);
            OpenTK.Graphics.OpenGL.GL.Translate((viewport[2] / 2) - x, (viewport[3] / 2) - y, 0f);

            Render(null, null);

            // reset OpenGL state
            OpenTK.Graphics.OpenGL.GL.MatrixMode(MatrixMode.Projection);
            OpenTK.Graphics.OpenGL.GL.PopMatrix();
            OpenTK.Graphics.OpenGL.GL.Flush();

            // Exit selection mode and return to render mode, returns number selected
            hits = OpenTK.Graphics.OpenGL.GL.RenderMode(RenderingMode.Render);

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

        protected Vector2 GetWorldCoordinates(int viewPortX, int viewPortY)
        {
            float worldX = viewPortX - (ClientRectangle.Width / 2);
            float worldY = viewPortY - (ClientRectangle.Height / 2);
            return new Vector2(worldX, worldY);
        }

    }
}
