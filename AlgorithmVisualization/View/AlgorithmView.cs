using System.Windows.Forms;
using AlgorithmVisualization.View.Util.Components;

namespace AlgorithmVisualization.View
{
#if DEBUG

        //For showing in Visual Studio Designer
        public class AlgorithmView : DoubleBufferedUserControl
        {
            public virtual Control VisualizationContainer { get; set; }
        }

#else

        abstract class AlgorithmViewBase : DoubleBufferedUserControl
        {
            public abstract Control VisualizationContainer { get; set; }
        }

#endif
}
