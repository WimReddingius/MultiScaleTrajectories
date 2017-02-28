using System;
using System.Windows.Forms;
using OpenTK;

namespace MultiScaleTrajectories.View.Visualization
{
    abstract class GLVisualization : GLControl
    {
        protected GLVisualization() : base(new OpenTK.Graphics.GraphicsMode(32, 24, 0, 8))
        {
            ParentChanged += (o, e) => MakeCurrent();
            Paint += Render;
        }

        protected abstract void Render(object sender, PaintEventArgs e);


    }
}
