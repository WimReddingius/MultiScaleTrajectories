using System.Windows.Forms;
using MultiScaleTrajectories.Algorithm.SingleTrajectory;
using MultiScaleTrajectories.Controller.Util;
using MultiScaleTrajectories.View.SingleTrajectory.Output;

namespace MultiScaleTrajectories.Controller.SingleTrajectory.Output
{
    class STOutputVisualizationController : DataViewController<STOutput>
    {
        protected STOutputVisualization OutputVis;

        public override Control OptionsControl => null;
        public override Control ViewControl => OutputVis;


        public STOutputVisualizationController()
        {
            OutputVis = new STOutputVisualization();
            DataLoaders.Add(OutputVis);
        }

        public override string ToString()
        {
            return "Visualization";
        }

    }
}
