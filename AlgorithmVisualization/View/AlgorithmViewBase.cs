using System.Windows.Forms;
using AlgorithmVisualization.View.Util.Components;

namespace AlgorithmVisualization.View
{
    public class AlgorithmViewBase : DoubleBufferedUserControl
    {
        public virtual Control VisualizationContainer { get; set; }
    }
}
