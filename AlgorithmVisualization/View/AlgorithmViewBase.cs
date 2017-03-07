using System.Windows.Forms;

namespace AlgorithmVisualization.View
{
    #if DEBUG

        //For showing in Visual Studio Designer
        public class AlgorithmViewBase : UserControl
        {
            public virtual Control VisualizationContainer { get; set; }
        }

    #else

        abstract class AlgorithmViewBase : UserControl
        {
            public abstract Control VisualizationContainer { get; set; }
        }

    #endif
}
