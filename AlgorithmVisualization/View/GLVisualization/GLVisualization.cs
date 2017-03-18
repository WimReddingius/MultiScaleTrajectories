using System;
using System.Windows.Forms;
using OpenTK;

namespace AlgorithmVisualization.View.GLVisualization
{
    public abstract class GLVisualization : GLControl
    {
        protected GLVisualization() : base(new OpenTK.Graphics.GraphicsMode(32, 24, 0, 8))
        {
            Paint += Render;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            MakeCurrent();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnLoad(e);

            MakeCurrent();
        }

        protected void Render(object sender, PaintEventArgs e)
        {
            MakeCurrent();

            RenderScene();
        }

        protected abstract void RenderScene();

    }
}
