using System.Windows.Forms;
using OpenTK;

namespace AlgorithmVisualization.View.Visualization
{
    public abstract class GLVisualization : GLControl
    {
        protected GLVisualization() : base(new OpenTK.Graphics.GraphicsMode(32, 24, 0, 8))
        {
            Paint += Render;
            ParentChanged += (o, e) => MakeCurrent();
        }

        protected abstract void Render(object sender, PaintEventArgs e);


    }
}
