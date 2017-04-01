using System.Windows.Forms;
using AlgorithmVisualization.View.Util.Components;

namespace AlgorithmVisualization.View
{
    public class AlgorithmView : DoubleBufferedUserControl
    {
        public virtual Control VisualizationContainer { get; set; }
    }
}
