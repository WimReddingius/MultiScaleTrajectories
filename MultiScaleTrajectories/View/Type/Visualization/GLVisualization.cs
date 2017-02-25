using MultiScaleTrajectories.Algorithm;
using OpenTK;
using System.Windows.Forms;

namespace MultiScaleTrajectories.View.Type.Visualization
{
    abstract class GLVisualization<IN, OUT> : GLControl
    {
        protected VisualizationMode Mode;

        protected AlgorithmRunner<IN, OUT> AlgorithmRunner;

        public GLVisualization(AlgorithmRunner<IN, OUT> runner) : base(new OpenTK.Graphics.GraphicsMode(32, 24, 0, 8))
        {
            CreateControl();

            Paint += new PaintEventHandler(this.Render);

            AlgorithmRunner = runner;

            SwitchMode(VisualizationMode.INPUT);
        }

        protected abstract void Render(object sender, PaintEventArgs e);

        protected virtual void SwitchMode(VisualizationMode newMode)
        {
            Mode = newMode;
            Refresh();
        }

        protected enum VisualizationMode
        {
            INPUT,
            OUTPUT
        }

    }
}
