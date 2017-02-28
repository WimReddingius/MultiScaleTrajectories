using System.Windows.Forms;
using MultiScaleTrajectories.Algorithm.SingleTrajectory;
using MultiScaleTrajectories.Controller.Util;
using MultiScaleTrajectories.View.SingleTrajectory.Output;

namespace MultiScaleTrajectories.Controller.SingleTrajectory.Output
{
    class STOutputVisualizationController : CompoundDataLoader<STOutput>, IOutputController
    {
        protected STOutputVisualization OutputVis;

        public Control OptionsControl => null;
        public Control ViewControl => OutputVis;


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
