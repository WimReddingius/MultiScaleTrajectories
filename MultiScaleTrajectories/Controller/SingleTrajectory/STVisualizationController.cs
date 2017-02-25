using System;
using System.Windows.Forms;
using MultiScaleTrajectories.Algorithm.SingleTrajectory;
using MultiScaleTrajectories.Algorithm;
using MultiScaleTrajectories.View.Type.Visualization.SingleTrajectory;

namespace MultiScaleTrajectories.Controller.SingleTrajectory
{
    partial class STVisualizationController : UserControl, ViewTypeController
    {
        protected AlgorithmRunner<STInput, STOutput> AlgorithmRunner;
        protected STVisualization STVisualization;

        public STVisualizationController(AlgorithmRunner<STInput, STOutput> runner)
        {
            InitializeComponent();

            AlgorithmRunner = runner;
            STVisualization = new STVisualization(runner);
            //make opengl context current when this control is readded
            STVisualization.ParentChanged += delegate (object sender, EventArgs e) { STVisualization.MakeCurrent(); };
        }

        public Control GetOptionsControl()
        {
            return this;
        }

        public Control GetViewControl()
        {
            return STVisualization;
        }

        public override string ToString()
        {
            return "Visualization";
        }
    }
}
