using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Experiment;
using AlgorithmVisualization.Controller.Explore;

namespace AlgorithmVisualization.View.Explore.Components.Log
{
    class LogExplorer<TIn, TOut> : SingularRunExplorerAfterState<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {
        public override string DisplayName => "Log";
        public override int Priority => 100;

        protected override RunState VisualizableState => RunState.Started;
        protected override RunStateReachedEventHandler<TIn, TOut> BeforeStateReachedHandler => visualization.BeforeStarted;
        protected override RunStateReachedEventHandler<TIn, TOut> AfterStateReachedHandler => visualization.AfterStarted;

        private readonly LogStream<TIn, TOut> visualization;


        public LogExplorer()
        {
            visualization = new LogStream<TIn, TOut>();
            WrapVisualization(visualization);
        }

    }
}
