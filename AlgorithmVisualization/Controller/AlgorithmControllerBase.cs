using AlgorithmVisualization.Util.Naming;
using AlgorithmVisualization.View;

namespace AlgorithmVisualization.Controller
{
    public abstract class AlgorithmControllerBase : Nameable
    {
        private AlgorithmViewBase algorithmView;
        public AlgorithmViewBase AlgorithmView => algorithmView ?? (algorithmView = CreateView());

        protected internal abstract AlgorithmViewBase CreateView();

    }
}
