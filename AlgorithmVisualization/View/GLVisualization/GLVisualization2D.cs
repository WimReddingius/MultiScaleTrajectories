using System;
using AlgorithmVisualization.View.GLVisualization.GLUtil;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace AlgorithmVisualization.View.GLVisualization
{
    public abstract class GLVisualization2D : GLVisualization
    {

        protected PickNameManager PickManager;

        protected double ZoomFactor;
        protected Vector2d WorldOrigin;


        protected GLVisualization2D()
        {
            PickManager = new PickNameManager();
            ZoomFactor = 1f;
            WorldOrigin = new Vector2d(0, 0);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(0.7f, 0.7f, 0.7f, 0.0f);  // Background
            GL.Enable(EnableCap.DepthTest);         //Enable correct Z Drawings
            GL.DepthFunc(DepthFunction.Always);     //Enable correct Z Drawings

            SetGLPerspective();

            GLTextRenderer2D.GenerateFontImage();
            GLTextRenderer2D.LoadTexture();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetGLPerspective();
        }

        private void SetGLPerspective()
        {
            GL.Viewport(0, 0, ClientRectangle.Width, ClientRectangle.Height);

            //setting up world coordinates
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, ClientRectangle.Width, 0, ClientRectangle.Height, 5, -5);
            GL.Translate((double)ClientRectangle.Width / 2, (double)ClientRectangle.Height / 2, 0.0);
        }

        protected override void RenderScene()
        {
            Clear();

            GL.MatrixMode(MatrixMode.Modelview);

            //world coordinates for world rendering: center of the screen corresponds to worldorigin
            GL.PushMatrix();
            GL.Scale(ZoomFactor, ZoomFactor, 1f);
            GL.Translate(-WorldOrigin.X, -WorldOrigin.Y, 0f);
            RenderWorld();
            GL.PopMatrix();

            //screen coordinates for hud rendering
            GL.PushMatrix();
            GL.Translate(-(double)ClientRectangle.Width / 2, (double)ClientRectangle.Height / 2, 0.0);
            GL.Rotate(180, new Vector3(0f, 0f, 1f));
            GL.Scale(-1f, 1f, 1f);
            RenderHud();
            GL.PopMatrix();

            SwapBuffers();
        }

        protected virtual void RenderWorld() { }

        protected virtual void RenderHud() { }

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

            // Set the buffer that OpenGL uses for selection to our buffer
            GL.SelectBuffer(256, buffer);

            // Change to selection mode
            GL.RenderMode(RenderingMode.Select);

            // InitializeView the name stack (used for identifying which object was selected)
            GL.InitNames();
            GL.PushName(-1);

            GL.MatrixMode(MatrixMode.Projection);
            GL.PushMatrix();

            /*  create pickRegionWidth x pickRegionHeight pixel picking region near cursor location */
            SetGLPerspective();

            var width = viewport[2];
            var height = viewport[3];
            GL.Scale(width / pickRegionWidth, height / pickRegionHeight, 1.0f);
            GL.Translate((width / 2) - x, y - (height / 2), 0f);

            RenderScene();

            // reset OpenGL state
            GL.MatrixMode(MatrixMode.Projection);
            GL.PopMatrix();
            GL.Flush();

            // Exit selection mode and return to render mode, returns number selected
            var hits = GL.RenderMode(RenderingMode.Render);

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

        protected Vector2d GetWorldCoordinates(int viewPortX, int viewPortY)
        {
            double worldX = 1 / ZoomFactor * (viewPortX - ClientRectangle.Width / 2) + WorldOrigin.X;
            double worldY = 1 / ZoomFactor * (ClientRectangle.Height / 2 - viewPortY) + WorldOrigin.Y;
            return new Vector2d(worldX, worldY);
        }

        protected void LookAt(double x, double y, double width, double height)
        {
            WorldOrigin = new Vector2d(x, y);
            ZoomFactor = Math.Min(ClientRectangle.Width / width, ClientRectangle.Height / height);
        }

    }
}
