﻿using AlgorithmVisualization.Algorithm.Experiment;
using AlgorithmVisualization.Controller.Explore;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;

namespace MultiScaleTrajectories.SingleTrajectory.View.Explore
{
    class STOutputExplorer : SingularRunExplorerSingleState<STInput, STOutput>
    {
        public override int Priority => 1;
        public override string DisplayName => "Node-Link Visualization";

        protected override RunState VisualizableState => RunState.OutputAvailable;
        protected override RunStateReachedEventHandler<STInput, STOutput> BeforeStateReachedHandler => Visualization.BeforeOutputAvailable;
        protected override RunStateReachedEventHandler<STInput, STOutput> AfterStateReachedHandler => Visualization.AfterOutputAvailable;

        public STOutputNodeLink Visualization;


        public STOutputExplorer()
        {
            Visualization = new STOutputNodeLink();
            WrapVisualization(Visualization);
        }

    }
}